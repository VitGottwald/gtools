﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace pserv4.windows
{
    public class WindowsDataController : DataController
    {
        private static List<ObjectColumn> ActualColumns;

        public WindowsDataController()
        {
        }

        public override IEnumerable<ObjectColumn> Columns
        {
            get
            {
                if (ActualColumns == null)
                {
                    ActualColumns = new List<ObjectColumn>();
                    ActualColumns.Add(new ObjectColumn(pserv4.Properties.Resources.WINDOW_C_Title, "Title"));
                    ActualColumns.Add(new ObjectColumn(pserv4.Properties.Resources.WINDOW_C_Class, "Class"));
                    ActualColumns.Add(new ObjectColumn(pserv4.Properties.Resources.WINDOW_C_Style, "Style"));
                    ActualColumns.Add(new ObjectColumn(pserv4.Properties.Resources.WINDOW_C_ExStyle, "ExStyle"));
                    ActualColumns.Add(new ObjectColumn(pserv4.Properties.Resources.WINDOW_C_ID, "ID"));
                    ActualColumns.Add(new ObjectColumn(pserv4.Properties.Resources.WINDOW_C_Size, "Size"));
                    ActualColumns.Add(new ObjectColumn(pserv4.Properties.Resources.WINDOW_C_Position, "Position"));
                    ActualColumns.Add(new ObjectColumn(pserv4.Properties.Resources.WINDOW_C_ProcessID, "ProcessID"));
                    ActualColumns.Add(new ObjectColumn(pserv4.Properties.Resources.WINDOW_C_ThreadID, "ThreadID"));
                    ActualColumns.Add(new ObjectColumn(pserv4.Properties.Resources.WINDOW_C_Process, "Process"));
                }
                return ActualColumns;
            }
        }

        public override void Refresh(ObservableCollection<DataObject> objects)
        {
            Dictionary<int, WindowDataObject> existingObjects = new Dictionary<int, WindowDataObject>();

            foreach (DataObject o in objects)
            {
                WindowDataObject sdo = o as WindowDataObject;
                if (sdo != null)
                {
                    existingObjects[sdo.Handle] = sdo;
                }
            }

            foreach (int hwnd in NativeWindowFunctions.EnumWindows() )
            {
                WindowDataObject wdo = null;

                if (existingObjects.TryGetValue(hwnd, out wdo))
                {
                    // todo: refresh existing instance from updated data
                }
                else
                {
                    wdo = new WindowDataObject(hwnd);
                    if (!string.IsNullOrEmpty(wdo.Title))
                    {
                        objects.Add(wdo);
                    }
                }
            }
        }
    }
}