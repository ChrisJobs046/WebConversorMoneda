using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebConversorMoneda.Models;
using System.Reflection.PortableExecutable;

namespace WebConversorMoneda.Controllers
{
    public enum Conversion
    {
        Peso,
        Dolar,
        Euro
    }

    public class ConversorController : Controller
    {
        //const float PESO_A_DOLAR = 0.018f;
        //const float PESO_A_EURO = 0.017f;
        const float DOLAR_A_PESO = 55.10f;
        const float DOLAR_A_EURO = 0.93f;
        //const float EURO_A_DOLAR = 0.0f;
        const float EURO_A_PESO = 59.41f;

        public IActionResult Index(string cantidad)
        {
            if (cantidad != null)
                return View(cantidad);

            return View();
        }

        [HttpPost]
        public IActionResult Convertir(string de, string a, float cantidad)
        {
            Conversion desde = (Conversion)Enum.Parse(typeof(Conversion), de);
            Conversion hasta = (Conversion)Enum.Parse(typeof(Conversion), a);

            switch (desde)
            {
                case Conversion.Dolar:
                    if (hasta.Equals(Conversion.Peso))
                    {
                        cantidad *= DOLAR_A_PESO;
                    }
                    else if (hasta.Equals(Conversion.Euro))
                    {
                        cantidad *= DOLAR_A_EURO;
                    }
                    break;
                case Conversion.Euro:
                    if (hasta.Equals(Conversion.Peso))
                    {
                        cantidad *= EURO_A_PESO;
                    }
                    else if (hasta.Equals(Conversion.Dolar))
                    {
                        cantidad *= 1 / DOLAR_A_EURO;
                    }
                    break;
                case Conversion.Peso:
                    if (hasta.Equals(Conversion.Dolar))
                    {
                        cantidad *= 1 / DOLAR_A_PESO;
                    }
                    else if (hasta.Equals(Conversion.Euro))
                    {
                        cantidad *= 1 / EURO_A_PESO;
                    }
                    break;
                default: break;
            }

            return View("Index", cantidad.ToString("c2"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}