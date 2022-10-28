package gov.revenue.vt6530;

import java.io.Closeable;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;

/**
 * Abstraction over the socket connection type, so we can
 * transparently support tls/plain connections.
 */
public class Connection implements Closeable {
    private Closeable socket;
    private InputStream inputStream;
    private OutputStream outputStream;

    public Closeable getSocket() {
        return socket;
    }

    public void setSocket(Closeable socket) {
        this.socket = socket;
    }

    public InputStream getInputStream() {
        return inputStream;
    }

    public void setInputStream(InputStream inputStream) {
        this.inputStream = inputStream;
    }

    public OutputStream getOutputStream() {
        return outputStream;
    }

    public void setOutputStream(OutputStream outputStream) {
        this.outputStream = outputStream;
    }

    @Override
    public void close() throws IOException {
        this.socket.close();
    }
}
