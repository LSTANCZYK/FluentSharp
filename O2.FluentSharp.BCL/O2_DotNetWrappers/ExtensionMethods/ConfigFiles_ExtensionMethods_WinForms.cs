﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.Kernel.ExtensionMethods;
using System.Drawing;

namespace O2.DotNetWrappers.ExtensionMethods
{
    public static class ConfigFiles_ExtensionMethods_WinForms
    {
        public static T editLocalConfigFile<T>(this string file, T control)
           where T : Control
        {
            var dataGridView = control.add_DataGridView()
                                      .allowNewRows()
                                      .allowRowsDeletion()
                                      .add_Columns("Key", "Value")
                                      .columnWidth(0, 200);
            var optionsPanel = dataGridView.insert_Above<GroupBox>(40).setText("Options").add_Panel();
            var config = file.localConfig_Load();
            if (config.isNull())
            {
                "Config file could not be loaded: {0}".error("config.xml".localScriptFile());
                optionsPanel.add_Label("Error: could not load config file:{0}".format(file)).foreColor(Color.Red);
                return control;
            }
            Label uiMessage = null;
            dataGridView.CellValueChanged += (sender, e) => { uiMessage.setText("Modified config settings").foreColor(Color.DarkRed); };
            uiMessage = optionsPanel.add_Link("Save", 0, 0,
                                        () =>
                                        {
                                            config.clear();
                                            foreach (var row in dataGridView.rows())
                                                if (row[0].str().valid() && row[1].str().valid())
                                                    config.add(row[0].str(), row[1].str());
                                            config.localConfig_Save(file);
                                            uiMessage.setText("Config file saved").foreColor(Color.DarkGreen);
                                        })
                                    .append_Label("Currenlty loaded config file:{0}".format(file)).autoSize()
                                    .append_Label("..").autoSize();
            foreach (var item in config)
                dataGridView.add_Row(item.Key, item.Value);

            return control;

        }
    }
}
