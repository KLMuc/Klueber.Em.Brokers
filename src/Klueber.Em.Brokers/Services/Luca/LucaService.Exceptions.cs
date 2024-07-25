using System;
using System.Threading.Tasks;
using Klueber.Em.Brokers.Models.ApiModels.Luca.Exceptions;

namespace Klueber.Em.Brokers.Services.Luca
{
    public partial class LucaService
    {
        private async ValueTask<T> TryCatch<T>(Func<ValueTask<T>> returningFunction)
        {
            try
            {
                return await returningFunction();
            }
            catch (LabServiceException labServiceException)
            {
                throw CreateAndLogValidationException(labServiceException);
            }
            catch (LabelNumberNullException labelNumberNullException)
            {
                throw CreateAndLogValidationException(labelNumberNullException);
            }
            catch (LabelNumberInvalidRangeException labelNumberValidationException)
            {
                throw CreateAndLogValidationException(labelNumberValidationException);
            }
            catch (OperatingTemperatureException operatingTemperatureException)
            {
                throw CreateAndLogValidationException(operatingTemperatureException);
            }
            catch (RequestSamplingDateException requestSamplingDateException)
            {
                throw CreateAndLogValidationException(requestSamplingDateException);
            }
            catch (IndexOutOfRangeException indexOutOfRangeException)
            {
                throw CreateAndLogValidationException(indexOutOfRangeException);
            }
            catch (RequestFieldNullException requestFieldNullException)
            {
                throw CreateAndLogValidationException(requestFieldNullException);
            }
            catch (CreateRequestNullException requestNullException)
            {
                throw CreateAndLogValidationException(requestNullException);
            }
            catch (LucaApiFailedMessageResultException lucaApiFailedMessageResultException)
            {
                throw CreateAndLogLucaServiceException(lucaApiFailedMessageResultException);
            }
            catch (Exception serviceException)
            {
                var failedLucaServiceException =
                    new LucaServiceException(serviceException);

                throw CreateAndLogLucaServiceException(failedLucaServiceException);
            }
        }

        private Exception CreateAndLogLucaServiceException(Exception exception)
        {
            var lucaServiceException =
                new LucaServiceException(exception);

            this.loggingBroker.LogError(lucaServiceException);

            return lucaServiceException;
        }

        private LucaValidationException CreateAndLogValidationException(Exception exception)
        {
            var lucaValidationException = new LucaValidationException(exception);

            this.loggingBroker.LogError(lucaValidationException);

            return lucaValidationException;
        }

    }
}
