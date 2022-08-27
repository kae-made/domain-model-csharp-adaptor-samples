using System;
// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.DomainModel.CSharp.Utilitiy.Application.WpfAppDomainModelViewer
{
    public interface DomainModelViewer
    {
        void RegisterInstance(string classKeyLetter, string nameOfIdentities, string identities);
        void UnregisterInstance(string classKeyLetter, string identities);
        void ShowInstance(string classKeyLetter, string identities);
    }
}
