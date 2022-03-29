using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using app.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace app.Contracts
{
    public sealed class SetStatusForm
    {
        [FromForm(Name = "Id")]
        public int UserId { get; set; }

        [FromForm(Name = "NewStatus")]
        public StatusDto Status { get; set; }
    }
}
