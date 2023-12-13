namespace Dido.PilotFAZ.Models;

public class Smtp2GoClient
{
    public string api_key { get; set; }
    public string sender { get; set; }
    public string[] to { get; set; }
    public string subject { get; set; }
    public string html_body { get; set; }
}