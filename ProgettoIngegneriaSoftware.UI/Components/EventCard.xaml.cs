using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgettoIngegneriaSoftware.Shared.Library.Models.DB_Models.Application;

namespace ProgettoIngegneriaSoftware.UI.Components;

public partial class EventCard : ContentView
{
    public EventCard()
    {
        InitializeComponent();
    }

    public EventModel? Event { get; set; }

}