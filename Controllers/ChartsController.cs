using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;



namespace MeterWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly DBLibraryContext _context;
        private readonly ClaimsPrincipal _user;

        public ChartsController(DBLibraryContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _user = accessor.HttpContext.User;

        }
        [HttpGet("JsonData2")]
        public JsonResult JsonData2()
        {
            var meterList = new List<object> { new[] { "Квартира", "Кількість лічильників" } };
            var flats = _context.Flats.Include(m => m.Meters).ToList();

            foreach (var f in flats)
            {
                meterList.Add(new object[] { f.FlatAddress, f.Meters.Count() });
            }

            return new JsonResult(meterList);
        }

        [HttpGet("JsonData")]

        public JsonResult JsonData()
        {
            var meterTypes = _context.MeterTypes.Include(m => m.Meters).ToList();

            List<object> typeMeter = new List<object>();
            typeMeter.Add(new[] { "Тип лічильника", "Кількість лічильників" });

            foreach(var t in meterTypes)
            {
                typeMeter.Add(new object[] { t.MeterTypeName, t.Meters.Count() });
            }
            return new JsonResult(typeMeter);

        }
        


    }
}
