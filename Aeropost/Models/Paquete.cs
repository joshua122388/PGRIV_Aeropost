using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public partial class Paquete
{
    public int Id { get; set; }               // esta es la  la PK 
    public string Tracking { get; set; } = "";
    public decimal Peso { get; set; }
    public decimal ValorTotal { get; set; }
    public string TiendaOrigen { get; set; } = ""; // los retailers 
    public bool EsEspecial { get; set; }      // productos especiales
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
    public string CedulaCliente { get; set; } = ""; // FK al cliente (o ClienteId 

    public void GenerarTracking()
    {
        // 1) 2 letras tienda
        var pref = new string((TiendaOrigen ?? "XX").ToUpper().Take(2).ToArray());
        // 2) mes 2 dígitos
        var mes = DateTime.Now.Month.ToString("00");
        // 3) año 2 dígitos
        var anio = (DateTime.Now.Year % 100).ToString("00");
        // 4) "MIA"
        // 5) 5 números aleatorios
        var rand = new Random().Next(0, 100000).ToString("00000");
        Tracking = $"{pref}{mes}{anio}MIA{rand}";
    }
}
