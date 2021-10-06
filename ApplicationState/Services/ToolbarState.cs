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

        // Call this from any page or component that wants to update the TabPage list
        public void UpdateTabPages(ComponentBase Source, List<TabPage> TabPages)
        {
            this.TabPages = TabPages;
            NotifyTabsChanged(Source);
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

        public event Action<ComponentBase> TabPagesUpdated;
        private void NotifyTabsChanged(ComponentBase Source) 
            => TabPagesUpdated?.Invoke(Source);
    
        
    }
}
