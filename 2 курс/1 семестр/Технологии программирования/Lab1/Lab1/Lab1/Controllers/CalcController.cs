using Lab1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    public class CalcController : Controller
    {
        public IActionResult Calc()
        {
            var model = new CalcModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Calculate(CalcModel model)
        {
            if (!string.IsNullOrEmpty(model.Expression) && model.Expression != "0")
            {
                try
                {
                    var resul = Convert.ToString(EvaluateExpression(model.Expression));
                    ViewBag.CalcResult = resul.ToString();
                    model.Result = resul.ToString();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Ошибка вычисления: {ex.Message}");
                    model.Result = "0";
                    ViewBag.CalculationResult = "Error";
                }
            }

            return View("Calc",model);
        }

        private double EvaluateExpression(string expression)
        {
            var dataTable = new System.Data.DataTable();
            var result = dataTable.Compute(expression, "");
            return Convert.ToDouble(result);
        }
    }
}
