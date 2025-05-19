using Flow.Core.Areas.Returns;
using Grpc.Core;

namespace FlowTutorials.ConsoleClient.Common.Extensions;

public static class GrpcFlowExtensions
{
    /*
        * For more more grpc control you can use client side grpc interceptors that you can register via the AddCodeFirstGrpcClient and the GrpcClientFactoryOptions.
        * This extension is just an example/alternate way of handling unexpected grpc issues, just like the message handler, you can use both.
    */
    public static async Task<Flow<T>> TryCatchGrpcResult<T>(this Task<Flow<T>> thisFlow)
    {
        try
        {
            return await thisFlow;
        }
        catch (RpcException rEx)
        {
            if (rEx.InnerException is OperationCanceledException) return Flow<T>.Failed(new Failure.TaskCancellationFailure("The request was cancelled or timed out, please try again.", exception: rEx));

            return Flow<T>.Failed(new Failure.UnknownFailure("A problem has occurred, please try again in a few minutes. If the problem persists please contact the system administrator.", exception: rEx));
        }

    }

}