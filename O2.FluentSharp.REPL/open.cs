using System.Windows.Forms;
using O2.DotNetWrappers.ExtensionMethods;
using O2.External.SharpDevelop.Ascx;
using O2.External.SharpDevelop.ExtensionMethods;
using O2.Kernel.CodeUtils;
using O2.Views.ASCX.CoreControls;
using O2.XRules.Database.Utils;
using O2.Core.XRules.Ascx;
using O2.Views.ASCX.classes.MainGUI;
using O2.DotNetWrappers.DotNet;
using System.Threading;
using O2.Views.ASCX.Ascx.MainGUI;

//O2File:ExtensionMethods/Reflection_ExtensionMethods.cs
//O2File:ExtensionMethods/Views_ExtensionMethods.cs
//O2File:CodeUtils/O2Kernel_Files.cs
//O2File:show.cs

namespace O2.Kernel
{
    public class open
    {
        public static ascx_SourceCodeEditor codeEditor()
        {
            return codeEditor("");
        }
        public static ascx_SourceCodeEditor codeEditor(string fileToOpen)
        {
            return fileToOpen.open_InCodeEditor();
        }
        public static Control               directory()
        {
            return "Directory Viewer".popupWindow(300,300)    
                                    .add_Control< ascx_Directory>()
                                    .simpleMode_withAddressBar();
        }
        public static Control               directory(string startDir)
        {
            return directory(startDir, true);
        }
        public static Control               directory(string startDir, bool watchFolder)
        {
            var control = directory();
            control.invoke("openDirectory", startDir);
            control.prop("_WatchFolder", watchFolder);
            return control;
        }
        public static TextBox               file(string fileToView)
        {
            var title = "Text file: " + fileToView;
            return title.popupWindow(800, 400)
                        .add_TextBox()
                        .set_Text(O2Kernel_Files.getFileContents(fileToView));            
        }
        public static RichTextBox           document(string fileToView)
        {
            var title = "RTF file: " + fileToView;
            return title.popupWindow(800, 400)
                        .add_RichTextBox()
                        .set_Rtf(O2Kernel_Files.getFileContents(fileToView));
        }        
        public static PictureBox            image(string imageToLoad)
        {
            var title = "image: " + imageToLoad;
            return title.popupWindow(800, 400)
                        .add_PictureBox()
                        .load(imageToLoad);
        }
        public static WebBrowser            web()
        {
            return webBrowser("");
        }
        public static WebBrowser            web(string url)
        {
            return webBrowser(url);
        }
        public static object                link(string url)
        {
            return webBrowser(url);
        }
        public static WebBrowser            browser()
        {
            return webBrowser("");
        }
        public static WebBrowser            browser(string url)
        {
            return webBrowser(url);
        }
        public static WebBrowser            webBrowser()
        {
            return webBrowser("");
        }
        public static WebBrowser            webBrowser(string url)
        {
			var browser = "Web Browser for: {0}".format(url).popupWindow().add_WebBrowser_with_NavigationBar();
			browser.open_ASync(url);
			return browser;
        }
        public static ascx_O2ObjectModel    o2ObjectModel()
        {
            return "O2 Object Model".popupWindow(500, 400)
                                    .add_Control<ascx_O2ObjectModel>();        
        }
        public static ascx_Panel_With_Inspector scriptEditor()
        {
            return ascx_Panel_With_Inspector.runControl();
        }
        public static ascx_Panel_With_Inspector scriptEditor(string file)
        {
            return ascx_Panel_With_Inspector.runControl(file);
        }        
        public static Thread scriptEditor_MtaThread()
        {
            return O2Thread.mtaThread(() => scriptEditor());
        }
        public static ascx_XRules_Editor devEnvironment()
        {
            return O2Gui.open<ascx_XRules_Editor>("O2 Development Environment",1024, 600);
        }
        public static Thread devEnvironment_MtaThread()
        {
            return O2Thread.mtaThread(() => devEnvironment());
        }
        public static ascx_LogViewer logViewer()
        {

            return O2Gui.open<ascx_LogViewer>();

        }
    }
}
