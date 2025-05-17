using Flow.Core.Areas.Extensions;
using Flow.Core.Areas.Returns;
using FlowTutorials.ConsoleClient.Common.Extensions;
using FlowTutorials.ConsoleClient.Common.Seeds;
using FlowTutorials.ConsoleClient.Common.Utilities;
using FlowTutorials.Contracts.Areas.Suppliers;
using System;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Text;

namespace FlowTutorials.ConsoleClient.Examples;

internal class Over_The_Wire_With_Json(IHttpClientFactory clientFactory) : IFlowExample
{
    public Range  PrintLineRange => 26 .. 62;
    public string FileName       => "05_Over_The_Wire_With_Json.cs";
    public int    Order          => 5;
    public string Name           => "Over the wire with Json";
    public string Description    => "Using flow over the wire with Json";

    public async Task RunExample()

        => await GetSupplierView(clientFactory.CreateClient(), GlobalValues.SuppliersViewPathFormat);
  

    private async Task GetSupplierView(HttpClient httpClient, string suppliersViewPathFormat)
    {
        var isAlive = await IsAlive(httpClient, "/suppliers");

        if (true == isAlive)
        {
            for (int index = 0; index < 2; index++)
            {

                var relativeUrl = index == 0 ? String.Format(suppliersViewPathFormat, Random.Shared.Next(30, 40)) : String.Format(suppliersViewPathFormat, Random.Shared.Next(1, 29));

                var flowResult = await httpClient.GetFromJsonAsync<Flow<SupplierView>>(relativeUrl)!
                                                        .OnFailure(failure => GeneralUtils.WriteLine($"{failure.Reason}\r\n", ConsoleColor.Red))
                                                            .OnSuccess(success => GeneralUtils.WriteLine($"{success}\r\n", ConsoleColor.Green))
                                                                .Finally(failure => null, success => success);
            }
        }
        /*
            * I use the approach below in my apps, the one above would need to wrapped in a try catch incase of timeouts or internet/server down errors.  
        */ 
        var anotherResult = await httpClient.GetAsync(String.Format(suppliersViewPathFormat, Random.Shared.Next(1, 30)))
                                                    .TryCatchJsonResult<SupplierView>()
                                                        .OnFailure(failure => GeneralUtils.WriteLine($"{failure.Reason}\r\n", ConsoleColor.Red))
                                                            .OnSuccess(success => GeneralUtils.WriteLine($"{success}\r\n", ConsoleColor.Green))
                                                                .Finally(failure => null, success => success);

    }

    private async Task<bool> IsAlive(HttpClient httpClient, string url)
    {
        try
        {
            return (await httpClient.GetAsync(url)) is null ? false : true;
        }
        catch { return false;}
    }

}
