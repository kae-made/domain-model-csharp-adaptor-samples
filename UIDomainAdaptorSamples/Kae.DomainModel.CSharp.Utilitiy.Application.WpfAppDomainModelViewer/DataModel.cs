// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Kae.DomainModel.Csharp.Framework.Adaptor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.DomainModel.CSharp.Utilitiy.Application.WpfAppDomainModelViewer
{
    class DataModelParam
    {
        public ParamSpec Spec { get; set; }
        public string Value { get; set; }
    }

    class DataModelClassProperty : INotifyPropertyChanged
    {
        public PropSpec Spec { get; set; }

        private string currentValue;
        public string Value
        {
            get { return currentValue; }
            set
            {
                currentValue = value;
                OnPropertyChanged("Value");
            }
        }

        private void OnPropertyChanged(string name)
        {
            
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
