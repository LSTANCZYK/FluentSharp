using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Interfaces.O2Core;
using O2.Interfaces.O2Findings;
using O2.Kernel;
using O2.DotNetWrappers.ExtensionMethods;
using System.Windows.Forms;
using O2.Views.ASCX.classes.MainGUI;
using O2.DotNetWrappers.DotNet;
using System.Drawing;
using O2.Views.ASCX.Ascx.MainGUI;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Threading;

namespace O2.DotNetWrappers.ExtensionMethods
{
    public static class WinForms_ExtensionMethods_Form
    {
        public static Form form(this string text)
        {
            return text.openForm();
        }
        public static Form openForm(this string text)
        {
            foreach (Form form in Application.OpenForms)
                if (form.get_Text() == text)
                    return form;
            return null;        
        }
        public static T form<T>(this FormCollection formCollection) where T : Form
        {
            var forms = new List<T>();
            foreach (Form form in Application.OpenForms)
                if (form is T)
                    return (T)form;
            return null;
        }
        public static List<T> forms<T>(this FormCollection formCollection) where T : Form
        {
            var forms = new List<T>();
            foreach (Form form in Application.OpenForms)
                if (form is T)
                    forms.Add((T)form);
            return forms;
        }
        public static List<T> applicationWinForms<T>(this object _object) where T : Form
        {
            return (from form in _object.applicationWinForms()
                    where form is T
                    select (T)form).toList();
        }
        public static List<Form> applicationWinForms(this object _object)
        {
            var forms = new List<Form>();
            foreach (Form form in Application.OpenForms)
                forms.Add(form);
            return forms;
            //foreach(var form in Application.OpenForms)
            //return (from form in 
            //		select form).toList();    		
        }

        public static Form lastFormLoaded(this string dummyString)
        {
            return dummyString.lastWindowShown();
        }
        public static Form lastWindowShown(this string dummyString)
        {
            return dummyString.applicationWinForms().Last();
        }
        public static Form opacity(this Form form, double value)
        {
            form.invokeOnThread(
                () =>
                {
                    form.Opacity = value;
                });
            return form;
        }
		
		
		public static T popupWindow<T>(this string title)
			where T : Control
        {
        	//title+=" - test" ;
            return title.showAsForm().add_Control<T>();
        }
        
        public static T popupWindow<T>(this string title, int width, int height)
        	where T : Control
        {
            return title.showAsForm(width, height)
            			.add_Control<T>();
        }
        
        public static Panel popupWindow(this string title)
        {
        	//title+=" - test" ;
            return title.showAsForm();
        }
        
        public static Panel popupWindow(this string title, int width, int height)
        {
            return title.showAsForm(width, height);
        }
        
        
        public static Panel createForm(this string title)
        {
            return title.showAsForm();
        }
        public static Panel showAsForm(this string title)
        {
            return title.showAsForm<Panel>(600, 400);
        }
        public static Panel showAsForm(this string title, int width, int height)
        {
            return O2Gui.open<Panel>(title, width, height);
        }

        public static T showAsForm<T>(this string title) where T : Control
        {
            return title.showAsForm<T>(600, 400);
        }
        public static T showAsForm<T>(this string title, int width, int height) where T : Control
        {
            return (T)O2Gui.open<T>(title, width, height)
                            .add_H2Icon();
        }

        public static T closeForm<T>(this T control) where T : Control
        {
            control.parentForm().close();
            return control;
        }
        public static T closeForm_InNSeconds<T>(this T control, int seconds) where T : Control
        {
            O2Thread.mtaThread(
                () =>
                {
                    control.sleep(seconds * 1000);
                    control.closeForm();
                });
            return control;
        }
        public static T resizeFormToControlSize<T>(this T control, Control controlToSync) where T : Control
        {
            if (controlToSync.notNull())
            {
                var parentForm = control.parentForm();
                if (parentForm.notNull())
                {
                    var top = controlToSync.PointToScreen(System.Drawing.Point.Empty).Y;
                    var left = controlToSync.PointToScreen(System.Drawing.Point.Empty).X;
                    var width = controlToSync.Width;
                    var height = controlToSync.Height;
                    "Setting parentForm location to {0}x{1} : {2}x{3}".info(top, left, width, height);
                    parentForm.Top = top;
                    parentForm.Left = left;
                    parentForm.Width = width;
                    parentForm.Height = height;
                }
            }
            return control;
        }
        public static Form onClosed<T>(this Form form, MethodInvoker onClosed)
        {
            if (form == null)
            {
                "in Form.onClosed, provided form value was null".error();
                return null;
            }
            form.Closed += (sender, e) => onClosed();
            return form;
        }
        public static Form close(this Form form)
        {
            form.invokeOnThread(() => form.Close());
            return form;
        }
        public static T waitForClose<T>(this T control) where T: Control
        {
            var form = control.parentForm();
            var formClosed = new AutoResetEvent(false);
            form.onClosed(() => formClosed.Set());
            formClosed.WaitOne();
            return control;
        }
        public static Form form_Currently_Running(this string titleOrType)
        {
            return titleOrType.applicationWinForm();
        }
        public static Form applicationWinForm(this string titleOrType)
        {
            return titleOrType.applicationWinForms().first();
        }
        public static List<Form> applicationWinForms(this string titleOrType)
        {
            var forms = new List<Form>();
            foreach (Form form in Application.OpenForms)
                if (form.get_Text() == titleOrType || form.str() == titleOrType ||
                    form.typeFullName() == titleOrType || form.typeName() == titleOrType)
                {
                    forms.Add(form);
                }
            return forms;
        }
        public static Form maximized(this Form form)
        {
            return (Form)form.invokeOnThread(
                () =>
                {
                    form.WindowState = FormWindowState.Maximized;
                    return form;
                });
        }
        public static T minimized<T>(this T control)            where T : Control
        {
            return control.windowState(FormWindowState.Minimized);
        }
        public static T maximized<T>(this T control)            where T : Control
        {
            return control.windowState(FormWindowState.Maximized);
        }
        public static T normal<T>(this T control)            where T : Control
        {
            return control.windowState(FormWindowState.Normal);
        }
        public static T windowState<T>(this T control, FormWindowState state)            where T : Control
        {
            return (T)control.invokeOnThread(
                () =>
                {
                    control.parentForm().WindowState = state;
                    return control;
                });
        }
        public static T parentForm_AlwaysOnTop<T>(this T control)			where T : Control
		{
			control.parentForm().alwaysOnTop();
			return control;
		}				
		public static T alwaysOnTop<T>(this T form)			where T : Form
		{
			form.invokeOnThread(()=> form.TopMost= true);
			return form;
		}

        //Icons
        public static Icon set_As_Default_Form_Icon(this Icon icon)
        {
            typeof(Form).fieldValue("defaultIcon", icon);
            return icon;
        }
        public static Icon asIcon(this Bitmap bitmap)
        {
            return Icon.FromHandle(bitmap.GetHicon());
        }
        public static Icon icon(this string iconFile)
		{
			try
			{
				return new Icon(iconFile);
			}
			catch(Exception ex)
			{
				"[icon] {0}".error(ex.Message);
				return null;
			}
		}
        public static string saveAs_Icon(this Bitmap bitmap)
        {
            return bitmap.saveAs_Icon(".ico".tempFile());
        }
        public static string saveAs_Icon(this Bitmap bitmap, string targetFile)
        {
            return bitmap.asIcon().saveAs(targetFile);
        }
        public static string save(this Icon icon)
        {            
            return icon.saveAs(".ico".tempFile());
        }
        public static string saveAs(this Icon icon, string targetFile)
        {
            using (FileStream fileStream = new FileStream(targetFile, FileMode.Create))
                icon.Save(fileStream);
            return targetFile;
        }
		public static T set_Form_Icon<T>(this T control, string iconFile)			where T : Control
		{
			return control.set_Form_Icon(iconFile.icon());
		}		
		public static T set_Form_Icon<T>(this T control, Icon icon)			where T : Control
		{
			control.parentForm().set_Icon(icon);
			return control;
		}
        public static T set_Icon<T>(this T control, string iconName) where T : Control
        {
            var file = iconName.local_Or_Resource();
            var parentForm = control.parentForm();
            if (file.fileExists() && parentForm.notNull())
            {
                parentForm.set_Icon(file.icon());
                return control;
            }
            "Error setting parent Form's Icon to: {0}".error(iconName);
            return control;
        }
        public static Form set_Icon(this Form form, Icon icon)
		{
			form.invokeOnThread(()=> form.Icon = icon);
			return form;
		}								
		public static T add_H2Icon<T>(this T control)			where T : Control
		{
			return control.set_Form_Icon("H2Logo.ico".local());
		}
        public static Form set_H2Icon(this Form form)	
		{
			return form.set_Icon("H2Logo.ico".local().icon());
		}
        public static Form set_O2Icon(this Form form)	
		{            
			return form.set_Icon("O2Logo.ico".local().icon());
		}
        public static Form set_DefaultIcon(this Form form)
        {
            try
            {
                var icon = Icon.ExtractAssociatedIcon(Assembly.GetEntryAssembly().Location);
                form.set_Icon(icon);
            }
            catch { }
            return form;        
        }
        public static Form clientSize(this Form form, int width, int height)
        {
            return (Form)form.invokeOnThread(() =>
                {
                    form.ClientSize = new Size(width, height);
                    return form;
                });
        }


    }

    public static class WinForms_ExtensionMethods_MDIForms
    {
        public static Form mdiForm(this string title)
        {
            return title.mdiHost();
        }

        public static Form mdiHost(this string title)
        {
            return title.popupWindow()
                        .parentForm()
                        .isMdiContainer();
        }

        public static Form isMdiContainer(this Form form)
        {
            return form.isMdiContainer(true);
        }

        public static Form isMdiContainer(this Form form, bool value)
        {
            return (Form)form.invokeOnThread(
                () =>
                {
                    form.Controls.Clear();
                    form.IsMdiContainer = true;
                    return form;
                });
        }

        public static Form add_MdiChild(this Form parentForm)
        {
            return parentForm.add_MdiChild("");
        }

        public static Form add_MdiChild(this Form parentForm, string title)
        {
            return parentForm.add_MdiChild<Form>(title);
        }

        public static T add_MdiChild<T>(this Form parentForm, string title)
            where T : Form
        {
            return (T)parentForm.invokeOnThread(
                () =>
                {
                    var mdiChild = (Form)typeof(T).ctor();
                    mdiChild.Text = title;
                    mdiChild.MdiParent = parentForm;
                    mdiChild.Show();
                    return mdiChild;
                });
        }

        public static Form add_MdiChild(this Form parentForm, Func<Form> formCtor)
        {
            return (Form)parentForm.invokeOnThread(
                () =>
                {
                    var mdiChild = formCtor();
                    mdiChild.MdiParent = parentForm;
                    mdiChild.Show();
                    return mdiChild;
                });
        }


        public static Form layout(this Form parentForm, MdiLayout layout)
        {
            return (Form)parentForm.invokeOnThread(
                () =>
                {
                    parentForm.LayoutMdi(layout);
                    return parentForm;
                });
        }

        public static Form layout_TileHorizontal(this Form parentForm)
        {
            return parentForm.layout(MdiLayout.TileHorizontal);
        }

        public static Form layout_TileVertical(this Form parentForm)
        {
            return parentForm.layout(MdiLayout.TileVertical);
        }

        public static Form layout_Cascade(this Form parentForm)
        {
            return parentForm.layout(MdiLayout.Cascade);
        }

        public static Form layout_ArrangeIcons(this Form parentForm)
        {
            return parentForm.layout(MdiLayout.ArrangeIcons);
        }

        public static Form tileHorizontaly(this Form parentForm)
        {
            return parentForm.layout(MdiLayout.TileHorizontal);
        }

        public static Form tileVerticaly(this Form parentForm)
        {
            return parentForm.layout(MdiLayout.TileVertical);
        }

        public static Form cascade(this Form parentForm)
        {
            return parentForm.layout(MdiLayout.Cascade);
        }

    }
}
