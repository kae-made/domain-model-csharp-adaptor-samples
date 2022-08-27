using Kae.DomainModel.Csharp.Framework;
using Kae.DomainModel.Csharp.Framework.Adaptor;
using Kae.Utility.Logging;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kae.DomainModel.CSharp.Utilitiy.Application.WpfAppDomainModelViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, DomainModelViewer
    {
        protected static Kae.Utility.Logging.Logger logger;
        protected static DomainModelAdaptor domainModelAdaptor;
        public static DomainModelAdaptor GetDomainModelAdaptor() { return domainModelAdaptor; }

        public MainWindow()
        {
            InitializeComponent();
            logger = new TextBlockLogger(tbLog);
            this.Loaded += MainWindow_Loaded;
        }

        Dictionary<string, Dictionary<string, ParamSpec>> domainOperationSpecs;
        Dictionary<string, ClassSpec> classSpecs;
        ObservableCollection<string> classInstances = new ObservableCollection<string>();

        Dictionary<string, DataModelParam> opParams = new Dictionary<string, DataModelParam>();
        string selectedOperation = "";
        string selectedClass = "";
        Dictionary<string, Dictionary<string, string>> instanceIdentities = new Dictionary<string, Dictionary<string, string>>();

        Dictionary<string, Dictionary<string, DomainModelInstanceViewer>> instanceWindows = new Dictionary<string, Dictionary<string, DomainModelInstanceViewer>>();
        
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            lbInstances.ItemsSource = classInstances;

        }

        protected void LoadDomainModelSpecification()
        {
            var adaptor = GetDomainModelAdaptor();
            lbClasses.ItemsSource = adaptor.ClassSpecs;
            lbDomainOperations.ItemsSource = adaptor.DomainOperationSpecs;

            classSpecs = adaptor.ClassSpecs;
            domainOperationSpecs = adaptor.DomainOperationSpecs;
        }

        protected void SetInstanceRepositoryUpdateHander()
        {
            var adaptor = (GetDomainModelAdaptor());
            adaptor.RegisterUpdateHandler(classPropertiesUpdated, relationshipUpdated);
        }

        protected DomainModelInstanceViewer GetWindowForTheInstance(string classKeyLetter, string identities)
        {
            DomainModelInstanceViewer windowOfInstance = null;
            if (instanceWindows.ContainsKey(classKeyLetter))
            {
                if (instanceWindows[classKeyLetter].ContainsKey(identities))
                {
                    windowOfInstance = instanceWindows[classKeyLetter][identities];
                }
            }
            return windowOfInstance;
        }

        protected void relationshipUpdated(object sender, RelationshipUpdatedEventArgs e)
        {
            var windowOfInstance = GetWindowForTheInstance(e.SourceClassKeyLetter, e.SourceIdentities);
            if (windowOfInstance != null)
            {
                windowOfInstance.UpdateData();
            }
            windowOfInstance = GetWindowForTheInstance(e.DestinationClassKeyLetter, e.DestinationIdentities);
            if (windowOfInstance != null)
            {
                windowOfInstance.UpdateData();
            }
        }

        protected void classPropertiesUpdated(object sender, ClassPropertiesUpdatedEventArgs e)
        {
            var windowOfInstance=GetWindowForTheInstance(e.ClassKeyLetter, e.Identities);
            if (windowOfInstance != null)
            {
                windowOfInstance.UpdateData();
            }
        }

        private void lbClasses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            classInstances.Clear();
            var selected = (KeyValuePair<string,ClassSpec>)lbClasses.SelectedItem;
            selectedClass = selected.Value.KeyLetter;
            var adaptor = GetDomainModelAdaptor();
            var instances = adaptor.GetInstances(selectedClass);
            var instancesJson = Newtonsoft.Json.JsonConvert.DeserializeObject(instances) as JArray;
            if (instancesJson != null)
            {
                foreach (var instance in instancesJson)
                {
                    string identities1 = "";
                    string identities = "";
                    var instanceJson = (JObject)instance;
                    foreach(var pk in classSpecs[selectedClass].Properties.Keys)
                    {
                        var procSpec = classSpecs[selectedClass].Properties[pk];
                        if (procSpec.Identity > 0)
                        {
                            if (!string.IsNullOrEmpty(identities))
                            {
                                identities += ",";
                            }
                            identities += $"{pk}={instanceJson[pk]}";
                            if (procSpec.Identity == 1)
                            {
                                if (!string.IsNullOrEmpty(identities1))
                                {
                                    identities1 += ",";
                                }
                                identities1+= $"{pk}={instanceJson[pk]}";
                            }
                        }
                    }
                    classInstances.Add($"{identities}");
                    RegisterInstance(selectedClass, identities, identities1);
                }
            }
        }

        private void lbDomainOperations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbOpEmptyParams.Visibility = Visibility.Hidden;
            var selected = (KeyValuePair<string, Dictionary<string, ParamSpec>>)lbDomainOperations.SelectedItem;
            selectedOperation = selected.Key;
            opParams.Clear();
            foreach(var pKey in selected.Value.Keys)
            {
                opParams.Add(pKey, new DataModelParam() { Spec = selected.Value[pKey] });
            }
            if (selected.Value.Keys.Count == 0)
            {
                tbOpEmptyParams.Visibility = Visibility.Visible;
            }
            lbInvokeOperations.ItemsSource = opParams;
        }

        private void lbInstances_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string targetIdentities = lbInstances.SelectedItem.ToString();
            string targetClassKey = selectedClass;
            var instanceWindow = FindOrCreateDomainModelInstanceViewer(targetIdentities, targetClassKey);
            instanceWindow.ShowView();
        }

        private DomainModelInstanceViewer FindOrCreateDomainModelInstanceViewer(string targetIdentities, string targetClassKey)
        {
            DomainModelInstanceViewer instanceWindow = null;
            string identities = instanceIdentities[targetClassKey][targetIdentities];
            if (!instanceWindows.ContainsKey(targetClassKey))
            {
                instanceWindows.Add(targetClassKey, new Dictionary<string, DomainModelInstanceViewer>());
            }
            if (instanceWindows[targetClassKey].ContainsKey(identities))
            {
                instanceWindow = instanceWindows[targetClassKey][identities];
            }
            else
            {
                instanceWindow = new WindowOfInstance(this, classSpecs[targetClassKey], identities, classSpecs);
                instanceWindows[targetClassKey].Add(identities, instanceWindow);
            }

            return instanceWindow;
        }

        private void buttonInvokeDomainOperation_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedOperation))
            {
                tbOperationResult.Text = "";
                var adaptor = GetDomainModelAdaptor();
                var invokeParams = new Dictionary<string, object>();
                foreach (var pk in opParams.Keys)
                {
                    bool isValid = false;
                    var rval = XFromStringToObj(opParams[pk].Value, opParams[pk].Spec.TypeKind, out isValid);
                    if (isValid)
                    {
                        invokeParams.Add(pk, rval);
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("Current supporting datatypes are 'Boolean', 'Integer', 'Real', 'String' and 'DateTime' only.");
                    }
                }
                var reqParams = new RequestingParameters() { Name = selectedOperation, Parameters = invokeParams };
                var result = adaptor.InvokeDomainOperation(selectedOperation, reqParams);
                tbOperationResult.Text = result;
            }
        }

        public static object XFromStringToObj(string data, ParamSpec.DataType dataType, out bool valid)
        {
            object result;
            valid = true;

            switch (dataType)
            {
                case ParamSpec.DataType.Boolean:
                    result = Convert.ToBoolean(data);
                    break;
                case ParamSpec.DataType.Integer:
                    result = Convert.ToInt32(data);
                    break;
                case ParamSpec.DataType.Real:
                    result = Convert.ToDouble(data);
                    break;
                case ParamSpec.DataType.String:
                    result = data;
                    break;
                case ParamSpec.DataType.DateTime:
                    result = Convert.ToDateTime(data);
                    break;
                default:
                    result = data;
                    valid = false;
                    break;
            }

            return result;
        }

        public void ShowInstance(string classKeyLetter, string identities)
        {
            var viewer = FindOrCreateDomainModelInstanceViewer(targetClassKey:classKeyLetter,targetIdentities:identities);
            viewer.ShowView();
        }

        public void RegisterInstance(string classKeyLetter, string nameOfIdentities, string identities)
        {
            if (!instanceIdentities.ContainsKey(classKeyLetter))
            {
                instanceIdentities.Add(classKeyLetter, new Dictionary<string, string>());
            }
            if (!instanceIdentities[classKeyLetter].ContainsKey(nameOfIdentities))
            {
                instanceIdentities[classKeyLetter].Add(nameOfIdentities, identities);
            }
        }

        public void UnregisterInstance(string classKeyLetter, string identities)
        {
            if (instanceWindows.ContainsKey(classKeyLetter))
            {
                if (instanceWindows[classKeyLetter].ContainsKey(identities))
                {
                    instanceWindows[classKeyLetter].Remove(identities);
                }
            }
        }

        private void buttonLoadDomainModelAdaptorDLL_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dilalog = new OpenFileDialog();
            dilalog.Filter = "DLL FIle|*.dll";
            if (dilalog.ShowDialog()==true)
            {
                tbDomainModelAdaptorDLL.Text = dilalog.FileName;
                var adaptorAssembly = Assembly.LoadFrom(tbDomainModelAdaptorDLL.Text);
                var loadedModules = adaptorAssembly.GetLoadedModules();
                if (loadedModules.Length > 0)
                {
                    var adaptorModule = loadedModules[0];
                    string nsOfAdaptor = adaptorModule.Name.Substring(0, adaptorModule.Name.LastIndexOf(".dll"));
                    var typeOfAdaptorEntry = adaptorModule.GetType($"{nsOfAdaptor}.Adaptor.DomainModelAdaptorEntry");
                    if (typeOfAdaptorEntry != null)
                    {
                        var methodOfGetAdaptor = typeOfAdaptorEntry.GetMethod("GetAdaptor");
                        if (methodOfGetAdaptor != null)
                        {
                            domainModelAdaptor = methodOfGetAdaptor.Invoke(null, new object[] { logger }) as DomainModelAdaptor;
                            if (domainModelAdaptor != null)
                            {
                                LoadDomainModelSpecification();
                                buttonLoadDomainModelAdaptorDLL.IsEnabled = false;
                                tbDomainName.Text = $"Domain Model : {domainModelAdaptor.DomainModelName}";
                            }
                        }
                    }
                }
            }

        }

        private void buttonSaveDomainModelState_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "JSON File|*.json";
            if (dialog.ShowDialog() == true)
            {
                tbDomainModelStateFile.Text = dialog.FileName;
                var adaptor = GetDomainModelAdaptor();
                string stateJson = adaptor.SaveDomainInstances();
                using (var writer = new StreamWriter(tbDomainModelStateFile.Text, false))
                {
                    writer.Write(stateJson);
                    writer.Flush();
                }
            }
        }

        private void buttonLoadDomainModelState_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JSON File|*.json";
            if (dialog.ShowDialog() == true)
            {
                tbDomainModelStateFile.Text = dialog.FileName;
                using (var reader = new StreamReader(tbDomainModelStateFile.Text))
                {
                    string stateJson = reader.ReadToEnd();
                    var adaptor = GetDomainModelAdaptor();
                    adaptor.LoadDomainInstances(stateJson);
                }
            }
        }
    }
    public class DomainDataTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = ParamSpec.DataType.Void;
            if (value is string)
            {
                result = (ParamSpec.DataType)Enum.Parse(typeof(ParamSpec.DataType), (string)value);
                return result;
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                return ((ParamSpec.DataType)Enum.ToObject(typeof(ParamSpec.DataType), (int)value)).ToString();
            }
            return DependencyProperty.UnsetValue;
        }
    }

}
