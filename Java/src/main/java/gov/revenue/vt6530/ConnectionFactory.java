package gov.revenue.vt6530;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.IOException;
import java.net.Socket;

import com.jcraft.jsch.JSchException;
import com.jcraft.jsch.UserInfo;

public class ConnectionFactory {

    private final String address;
    private final int port;

    public ConnectionFactory(String address, int port) {
        this.address = address;
        this.port = port;
    }

    public Connection createConnection() {
//        // FIXME: move into a socket factory of some sort:
//        socket = SSLSocketFactory.getDefault().createSocket(address, port);
//        // socket = new Socket(address, port);
//        is = new BufferedInputStream(socket.getInputStream());
//        os = new BufferedOutputStream(socket.getOutputStream());
        return createSshConnection();

        // return createBasicTest();
    }

    private Connection createSshConnection() {
        Connection result = new Connection();

        try {
            com.jcraft.jsch.JSch ssh = new com.jcraft.jsch.JSch();


            com.jcraft.jsch.Session session = null;
            // FIXME
            session = ssh.getSession("myuser", address, port);

            // FIXME
            session.setConfig("StrictHostKeyChecking", "no");

            // FIXME
            UserInfo myUserInfo = new UserInfo() {
                @Override
                public String getPassphrase() {
                    return null;
                }

                @Override
                public String getPassword() {
                    return "123";
                }

                @Override
                public boolean promptPassword(String s) {
                    return true;
                }

                @Override
                public boolean promptPassphrase(String s) {
                    return false;
                }

                @Override
                public boolean promptYesNo(String s) {
                    return false;
                }

                @Override
                public void showMessage(String s) {

                }
            };
            session.setUserInfo(myUserInfo); // authentication

            session.connect();
            com.jcraft.jsch.ChannelSubsystem subsystem = (com.jcraft.jsch.ChannelSubsystem) session.openChannel("subsystem");
            subsystem.setSubsystem("tacl");

            result.setInputStream(new BufferedInputStream(subsystem.getInputStream()));
            result.setOutputStream(new BufferedOutputStream(subsystem.getOutputStream()));

//            subsystem.getInputStream();
//            subsystem.getErrStream();
//            subsystem.getOutputStream();
            subsystem.connect();
        } catch (JSchException | IOException e) {
            throw new RuntimeException(e);
        }

        return result;
    }

    private Connection createBasicTest() {
        Connection result = new Connection();

        try {
            Socket socket = new Socket(address, port);
            result.setSocket(socket);
            result.setInputStream(new BufferedInputStream(socket.getInputStream()));
            result.setOutputStream(new BufferedOutputStream(socket.getOutputStream()));
        } catch (IOException e) {
            throw new RuntimeException(e);
        }

        return result;
    }
}
