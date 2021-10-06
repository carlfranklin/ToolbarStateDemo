using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using ApplicationState.Shared;

namespace ApplicationState.Services
{
    public class ToolbarState 
    {
        // All the toolbar items are locaed here.
        public List<TabPage> TabPages { get; set; } = new List<TabPage>();

        // Call this from any page or component that wants to swap one TabPage for another
        public void UpdateTabPage(ComponentBase Source, TabPage ExistingTabPage, TabPage NewTabPage)
        {
            // We need the index in order to update it.
            var found = (from x in TabPages
                         where x.Text == ExistingTabPage.Text
                         && x.Value == ExistingTabPage.Value
                         select x).FirstOrDefault();
            if (found != null)
            {
                // Get the index
                var index = TabPages.IndexOf(found);
                // Update the TabPage in the List
                TabPages[index] = NewTabPage;
                // Notify subscribers that this has changed
                NotifyTabChanged(Source, ExistingTabPage, NewTabPage);
            }
        }

        // This will be called from the toolbar when someone clicks on a button
        public void SendTabClicked(ComponentBase Source, TabPage TabPage)
        {
            // Notify subscribers that a button has been clicked on
            NotifyTabClicked(Source, TabPage);
        }
                
        public event Action<ComponentBase, TabPage> TabPageClicked;
        private void NotifyTabClicked(ComponentBase Source, TabPage TabPage)
            => TabPageClicked?.Invoke(Source, TabPage);

        public event Action<ComponentBase, TabPage, TabPage> TabPageUpdated;
        private void NotifyTabChanged(ComponentBase Source, TabPage ExistingTabPage, TabPage NewTabPage) 
            => TabPageUpdated?.Invoke(Source, ExistingTabPage, NewTabPage);
    
        
    }
}
