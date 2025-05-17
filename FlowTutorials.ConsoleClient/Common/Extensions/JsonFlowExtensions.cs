using Flow.Core.Areas.Returns;
using System.Net.Http.Json;
using System.Text.Json;

namespace FlowTutorials.ConsoleClient.Common.Extensions;

public static class JsonFlowExt
{

    public static async Task<Flow<T>> TryCatchJsonResult<T>(this Task<HttpResponseMessage> @this)
    {
        try
        {
            var response = await @this;
            /*
                * If you are using and serializing a flow from the server then the failure type should have been set on the server.
                * i.e there is probably no need to check the status code, but you could do that if necessary.
             */

            Type resultType = typeof(Flow<>).MakeGenericType(typeof(T));

            return (Flow<T>)(await response.Content.ReadFromJsonAsync(resultType))!;

        }
        catch (OperationCanceledException oEx)
        { 
            return Flow<T>.Failed(new Failure.TaskCancellationFailure(oEx.Message));
        }
        catch (Exception ex)
        {
            /*
                * If connection failure/no server up and running. 
            */
            return Flow<T>.Failed(new Failure.UnknownFailure("A problem has occurred, please try again in a few minutes. If the problem persists please contact the system administrator.", exception: ex));
        }

    }

}