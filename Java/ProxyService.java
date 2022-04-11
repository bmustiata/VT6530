import java.io.*;
import com.ms.service.*;

public class ProxyService extends Service
{
    static
    {
        // Uncomment to disable the assassin.  The service will fail to respond 
        // in the time specified in the last waithint for the third pause
        // event received.  If the assassin is enabled (i.e. this line is commented
        // out, the default), then the service will be forcibly killed.
        
        //Service.disableassassin = true;
    }

	Thread serviceThread;
	ProxyThread nsThread;
	
    public ProxyService (String[] args) throws IOException
    {
        //System.out.println("Sending updated pending status");

        CheckPoint(1000);

        //System.out.println("Sending running status with all controls");

        setRunning(ACCEPT_SHUTDOWN | ACCEPT_STOP);
        
		nsThread = new ProxyThread();
		serviceThread = new Thread(nsThread);
		serviceThread.start();
		
        System.out.println("Started");
    }

    protected boolean handleStop ()
    {
        setStopping(5000);
        //System.out.println("stoping");
		serviceThread.stop();
        return true;
    }

    protected boolean handleShutdown ()
    {
        //System.out.println("received shutdown, treating as stop");
        return handleStop();
    }

    protected boolean handleInterrogate ()
    {
        setServiceStatus(getServiceStatus());
        
        System.out.println("stopping");
        StopServiceEventHandler(1000);
        
        return false;
    }
}

class ProxyThread implements Runnable
{
	boolean running = false;
	
	public void run()
	{
		running = true;
		String[] args = new String[3];
		args[0] = "900";
		args[1] = "is";
		args[2] = "1016";
		Proxy.main(args);
	}	
}
