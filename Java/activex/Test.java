package activex;

import com.ms.wfc.app.*;
import com.ms.wfc.core.*;
import com.ms.wfc.ui.*;
import com.ms.wfc.html.*;

/**
 * This class can take a variable number of parameters on the command
 * line. Program execution begins with the main() method. The class
 * constructor is not invoked unless an object of type 'Test'
 * created in the main() method.
 */
public class Test extends Form
{
	public Test()
	{
		super();

		// Required for Visual J++ Form Designer support
		initForm();		

		// TODO: Add any constructor code after initForm call
	}

	/**
	 * Test overrides dispose so it can clean up the
	 * component list.
	 */
	public void dispose()
	{
		super.dispose();
		components.dispose();
	}

	private void btnConnect_click(Object source, Event e)
	{
		vt6530Control1.connect();
	}

	/**
	 * NOTE: The following code is required by the Visual J++ form
	 * designer.  It can be modified using the form editor.  Do not
	 * modify it using the code editor.
	 */
	Container components = new Container();
	Vt6530Control vt6530Control1 = new Vt6530Control();
	Button btnConnect = new Button();

	private void initForm()
	{
		this.setText("Test");
		this.setAutoScaleBaseSize(new Point(5, 13));
		this.setClientSize(new Point(657, 448));

		vt6530Control1.setAnchor(ControlAnchor.ALL);
		vt6530Control1.setBackColor(Color.BLACK);
		vt6530Control1.setFont(new Font(null, 15.888573f, FontSize.PIXELS, FontWeight.NORMAL, false, false, false, CharacterSet.DEFAULT, 0));
		vt6530Control1.setForeColor(Color.GREEN);
		vt6530Control1.setLocation(new Point(8, 0));
		vt6530Control1.setSize(new Point(640, 400));
		vt6530Control1.setTabIndex(0);
		vt6530Control1.setText("vt6530Control1");
		vt6530Control1.setHost(null);
		vt6530Control1.setPort(0);

		btnConnect.setAnchor(ControlAnchor.BOTTOMLEFT);
		btnConnect.setLocation(new Point(8, 408));
		btnConnect.setSize(new Point(64, 32));
		btnConnect.setTabIndex(1);
		btnConnect.setText("Connect");
		btnConnect.addOnClick(new EventHandler(this.btnConnect_click));

		this.setNewControls(new Control[] {
							btnConnect, 
							vt6530Control1});
	}

	/**
	 * The main entry point for the application. 
	 *
	 * @param args Array of parameters passed to the application
	 * via the command line.
	 */
	public static void main(String args[])
	{
		Application.run(new Test());
	}
}
