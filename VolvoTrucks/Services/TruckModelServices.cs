using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolvoTrucks.Models;

namespace VolvoTrucks.Services
{
    public static class TruckModelServices
    {
        public static List<SelectListItem> GetSelectListItems()
        {
            var selectList = new List<SelectListItem>();
            using(var context = new VolvoTrucksContext())
            {
                var list = context.TruckModels.OrderBy(x => x.ModelName).ToList();

                list.ForEach(x =>
                {
                    selectList.Add(new SelectListItem
                    {
                        Value = x.ModelId.ToString(),
                        Text = x.ModelName
                    });
                });
            }

            return selectList;
        }
    }
}
