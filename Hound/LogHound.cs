using Hound.Config;
using Hound.Result;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hound
{
    public static class LogHound
    {
        public static HoundResult LogException(Exception exception)
        {
            try
            {
                HoundException houndException = CastException(exception);
                return Task.Run(async () => await Log(Configuration.GetApiKey()).Publish(houndException, Configuration.GetTags())).Result;
            }
            catch (Exception ex)
            {
                return HoundResultMapper.GetFailureResponse(ex);
            }
        }

        [Obsolete("This overload is deprecated, please specify the datadog api key in config using the key 'DataDog_Api_Key'")]
        public static HoundResult LogException(string apiKey, Exception exception)
        {
            try
            {
                HoundException houndException = CastException(exception);
                return Task.Run(async() => await Log(apiKey).Publish(houndException)).Result;
            }
            catch (Exception ex)
            {
                return HoundResultMapper.GetFailureResponse(ex);
            }
        }

        [Obsolete("This overload is deprecated, please specify the tags you wish to use in config using the key 'DataDog_Tags'")]
        public static HoundResult LogException(string apiKey, Exception exception, IEnumerable<string> tags)
        {
            try
            {
                HoundException houndException = CastException(exception);
                return Task.Run(async() => await Log(apiKey).Publish(houndException, tags)).Result;
            }
            catch (Exception ex)
            {
                return HoundResultMapper.GetFailureResponse(ex);
            }
        }

        private static HoundException CastException(Exception exception)
        {
            HoundException houndException = exception as HoundException;

            if (houndException == null)
            {
                houndException = new HoundException(exception);
            }

            return houndException;
        }

        private static IExceptionDestination Log(string apiKey)
        {
            IEventDestination eventDestination = new DogEvents(apiKey);
            return new DogExceptions(eventDestination);
        }
    }
}