// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Microsoft.AspNetCore.SignalR;

namespace Kae.DomainModel.CSharp.Utility.Application.WebAPIAppDomainModelViewer.Hubs
{
    public class DomainModelStateHub : Hub
    {
        public void InstanceUpdated(string classKeyLetter, string operation, string identities, IDictionary<string, object> changedProperties)
        {
            var message = new
            {
                ClassKeyLetter = classKeyLetter,
                Operation = operation,
                Identities = identities,
                ChnagedProperties = changedProperties
            };
            string sendMessage = Newtonsoft.Json.JsonConvert.SerializeObject(message);
            Clients.All.SendAsync("NotifyInstanceUpdated", sendMessage).Wait();
        }

        public void RelationshipUpdated(string relId, string phrase, string sourceClassKeyLetter, string sourceIdentities, string destinationClassKeyLetter, string destinationIdentities)
        {
            var message = new
            {
                Relationship = new
                {
                    RelationshipId = relId,
                    Phrase = phrase,
                },
                Source = new
                {
                    ClassKeyLetter = sourceClassKeyLetter,
                    Identities = sourceIdentities,
                },
                Destination = new
                {
                    ClassKeyLetter = destinationClassKeyLetter,
                    Identities = destinationIdentities
                }
            };
            string sendMessage = Newtonsoft.Json.JsonConvert.SerializeObject(message);
            Clients.All.SendAsync("NotifyRelationshipUpdated", sendMessage);
        }
    }
}
