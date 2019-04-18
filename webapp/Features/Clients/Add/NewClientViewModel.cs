using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webapp.Models;
using webapp.Models.ViewModels;

namespace webapp.Features.Clients.Add
{
    public class NewClientViewModel
    {
        public AddActionAdditionalData Data { get; set; }
        public BusinessData Model { get; set; }
    }
}