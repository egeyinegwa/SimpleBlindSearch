using Blinds.com.Data;
using Blinds.com.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Blinds.com.ControllersWebAPI
{
    public class BlindController : ApiController
    {
        [HttpGet]
        public ManageBlindVM GetBlinds(string selectedBlind, string orderBy)
        {
            ManageBlindVM manageBlindVM = new ManageBlindVM();
            manageBlindVM.Items         = new List<BlindVM>();

            var selectedBlinds          = BlindData.GetBlinds(50).Where(b => b.Name.Contains(selectedBlind));

            var categories              = selectedBlinds.Select(b => b.Category).Distinct().ToList();

            categories.ForEach(c =>
                {
                    BlindVM blindVM      = new BlindVM();
                    blindVM.Category     = c;
                    var blindDetailsVM   = selectedBlinds.Where(b => b.Category == c)
                                                         .Select(b => new BlindDetailVM
                                                                        {
                                                                            Name =  b.Name,
                                                                            Price = b.Price
                                                                        });
                    if(orderBy == "Price")
                        blindVM.Blinds = blindDetailsVM.OrderBy(bd => bd.Price);
                    else
                        blindVM.Blinds = blindDetailsVM.OrderBy(bd => bd.Name);                

                    manageBlindVM.Items.Add(blindVM);                    
                });

            return manageBlindVM;
        }   
    }
}
