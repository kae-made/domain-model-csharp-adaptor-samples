// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Kae.DomainModel.Csharp.Framework.Adaptor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kae.DomainModel.CSharp.Utilitiy.Application.WpfAppDomainModelViewer
{
    /// <summary>
    /// WindowOfInstance.xaml の相互作用ロジック
    /// </summary>
    public partial class WindowOfInstance : Window, DomainModelInstanceViewer
    {
        DomainModelViewer ownerViewer;
        Dictionary<string, ClassSpec> classSpecs;
        ClassSpec currentSpec;

        string identitiesStr = "";
        Dictionary<string, string> identities = new Dictionary<string, string>();
        ObservableCollection<DataModelClassProperty> instanceProperties = new ObservableCollection<DataModelClassProperty>();
        ObservableCollection<string> linkedInstances = new ObservableCollection<string>();

        Dictionary<string, DataModelParam> opParams = new Dictionary<string, DataModelParam>();
        Dictionary<string, DataModelParam> evtParams = new Dictionary<string, DataModelParam>();
        string selectedOperation = "";
        string selectedEvent = "";
        string selectedRelName = "";
        string selectedRelDstKey = "";

        List<DataModelClassProperty> originalProperties = new List<DataModelClassProperty>();

        public WindowOfInstance(DomainModelViewer viewer, ClassSpec spec, string identities, Dictionary<string, ClassSpec> classSpecs)
        {
            this.ownerViewer = viewer;
            this.currentSpec = spec;
            this.classSpecs = classSpecs;
            this.identitiesStr = identities;

            InitializeComponent();
            this.Loaded += WindowOfInstance_Loaded;

            var items = identities.Split(new char[] { ',' });
            foreach(var item in items)
            {
                var frags = item.Split(new char[] { '=' });
                this.identities.Add(frags[0], frags[1]);
            }

            this.Closing += WindowOfInstance_Closing;
        }

        private void WindowOfInstance_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ownerViewer.UnregisterInstance(currentSpec.KeyLetter, identitiesStr);
        }

        private void WindowOfInstance_Loaded(object sender, RoutedEventArgs e)
        {
            lbNameOfPropsOfInstance.ItemsSource = currentSpec.Properties;
            lbValueOfPropsOfInstances.ItemsSource = instanceProperties;
            lbLinkedInstances.ItemsSource = linkedInstances;

            LoadCurrentProperties();
            lbClassOperations.ItemsSource = currentSpec.Operations;
            lbClassEvetns.ItemsSource = currentSpec.Events;
            lbRelationships.ItemsSource = currentSpec.Links;
        }

        private void LoadCurrentProperties()
        {
            var adaptor = MainWindow.GetDomainModelAdaptor();
            var instance = adaptor.GetInstance(currentSpec.KeyLetter, identities);
            var instanceJson = Newtonsoft.Json.JsonConvert.DeserializeObject(instance) as JObject;
            foreach (var pk in currentSpec.Properties.Keys)
            {
                var propSpec = currentSpec.Properties[pk];
                string propValue = "";
                if (instanceJson[pk] != null)
                {
                    propValue = $"{instanceJson[pk]}";
                }
                instanceProperties.Add(new DataModelClassProperty() { Spec = propSpec, Value = propValue });
                originalProperties.Add(new DataModelClassProperty() { Spec = propSpec, Value = propValue });
            }
        }

        private void buttonUpdateProperties_Click(object sender, RoutedEventArgs e)
        {
            var adaptor = MainWindow.GetDomainModelAdaptor();
            var invokeParams = new Dictionary<string, object>();
            for (int index = 0; index < instanceProperties.Count; index++)
            {
                if (instanceProperties[index].Spec.Writable)
                {
                    if (instanceProperties[index] != originalProperties[index])
                    {
                        bool isValid = false;
                        var updatedValue = MainWindow.XFromStringToObj(instanceProperties[index].Value, instanceProperties[index].Spec.DataType, out isValid);
                        if (isValid)
                        {
                            invokeParams.Add(instanceProperties[index].Spec.Name, updatedValue);
                            originalProperties[index] = instanceProperties[index];
                        }
                    }
                }
            }
            var reqParams = new RequestingParameters() { Identities=identities, Parameters=invokeParams };
            var result = adaptor.UpdateClassProperties(currentSpec.KeyLetter, reqParams);
        }

        private void lbClassOperations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbOpNoParams.Visibility = Visibility.Hidden;
            var selected = (KeyValuePair<string, OperationSpec>)lbClassOperations.SelectedItem;
            selectedOperation = selected.Key;
            opParams.Clear();
            foreach (var pKey in selected.Value.Parameters.Keys)
            {
               opParams.Add(pKey, new DataModelParam() { Spec = selected.Value.Parameters[pKey] });
            }
            if (selected.Value.Parameters.Count == 0)
            {
                tbOpNoParams.Visibility = Visibility.Visible;
            }
            lbClassOperationParams.ItemsSource = opParams;
        }

        private void buttonInvokeClassOperation_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedOperation))
            {
                tbOperationResult.Text = "";
                var adaptor = MainWindow.GetDomainModelAdaptor();
                var invokeParams = new Dictionary<string, object>();
                foreach (var pk in opParams.Keys)
                {
                    bool isValid = false;
                    var rval = MainWindow.XFromStringToObj(opParams[pk].Value, opParams[pk].Spec.TypeKind, out isValid);
                    if (isValid)
                    {
                        invokeParams.Add(pk, rval);
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("Current supporting datatypes are 'Boolean', 'Integer', 'Real', 'String' and 'DateTime' only.");
                    }
                }
                var reqParams = new RequestingParameters() { Name = selectedOperation, Parameters = invokeParams, Identities=identities };
                var result = adaptor.InvokeDomainClassOperation(currentSpec.KeyLetter, selectedOperation, reqParams);
                tbOperationResult.Text = result;
            }
        }
        private void lbClassEvetns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbEvtNoParams.Visibility = Visibility.Hidden;
            var selected = (KeyValuePair<string, OperationSpec>)lbClassEvetns.SelectedItem;
            selectedEvent = selected.Key;
            evtParams.Clear();
            foreach (var pKey in selected.Value.Parameters.Keys)
            {
                evtParams.Add(pKey, new DataModelParam() { Spec = selected.Value.Parameters[pKey] });
            }
            if (selected.Value.Parameters.Keys.Count == 0)
            {
                tbEvtNoParams.Visibility = Visibility.Visible;
            }
            lbEventParams.ItemsSource = evtParams;
        }

        private void buttonSendEvent_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedEvent))
            {
                var adaptor = MainWindow.GetDomainModelAdaptor();
                var invokeParams = new Dictionary<string, object>();
                foreach (var pk in evtParams.Keys)
                {
                    bool isValid = false;
                    var rval = MainWindow.XFromStringToObj(evtParams[pk].Value, evtParams[pk].Spec.TypeKind, out isValid);
                    if (isValid)
                    {
                        invokeParams.Add(pk, rval);
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("Current supporting datatypes are 'Boolean', 'Integer', 'Real', 'String' and 'DateTime' only.");
                    }
                }
                var reqParams = new RequestingParameters() { Name = selectedOperation, Parameters = invokeParams, Identities = identities };
                var result = adaptor.SendEvent(currentSpec.KeyLetter, selectedEvent, reqParams);
                tbOperationResult.Text = result;
            }
        }

        private void lbRelationships_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            linkedInstances.Clear();
            tbLinkedInstanceState.Visibility = Visibility.Hidden;
            var selected = (KeyValuePair<string, LinkSpec>)lbRelationships.SelectedItem;
            selectedRelName = selected.Value.Name;
            selectedRelDstKey = selected.Value.DstKeyLett;
            LoadCurrentLinkedInstances();
        }

        private void LoadCurrentLinkedInstances()
        {
            if (!string.IsNullOrEmpty(selectedRelName))
            {
                var adaptor = MainWindow.GetDomainModelAdaptor();
                var lis = adaptor.GetLinkedInstances(currentSpec.KeyLetter, identities, selectedRelName);
                var linkedInstancesJson = Newtonsoft.Json.JsonConvert.DeserializeObject(lis) as JArray;
                foreach (var liJson in linkedInstancesJson)
                {
                    string identities = "";
                    string identities1 = "";
                    var linkedInstanceJson = (JObject)liJson;
                    var dstClassProps = classSpecs[selectedRelDstKey].Properties;
                    foreach (var pk in dstClassProps.Keys)
                    {
                        if (dstClassProps[pk].Identity > 0)
                        {
                            if (!string.IsNullOrEmpty(identities))
                            {
                                identities += ",";
                            }
                            identities += $"{pk}={linkedInstanceJson[pk]}";
                            if (dstClassProps[pk].Identity == 1)
                            {
                                if (!string.IsNullOrEmpty(identities1))
                                {
                                    identities1 += ",";
                                }
                                identities1 += $"{pk}={linkedInstanceJson[pk]}";
                            }
                        }
                    }
                    ownerViewer.RegisterInstance(selectedRelDstKey, identities, identities1);
                    linkedInstances.Add(identities);
                }
                if (linkedInstances.Count == 0)
                {
                    tbLinkedInstanceState.Visibility = Visibility.Visible;
                }
            }
        }

        private void lbLinkedInstances_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedInstance = (string)lbLinkedInstances.SelectedItem;
            ownerViewer.ShowInstance(selectedRelDstKey, selectedInstance);
        }

        public void UpdateData()
        {
            LoadCurrentProperties();
            LoadCurrentLinkedInstances();
        }

        public void ShowView()
        {
            this.Show();
            if (this.IsActive == false)
            {
                this.Activate();
            }
        }
    }

}
