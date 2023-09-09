using System.Collections.Generic;
using YG;

namespace Yandex
{
    public static class Metrica
    {
        public static void SendMetricMessage(string eventName, IDictionary<string, string> message)
        {
            YandexMetrica.Send(eventName, message);
        }
    }
}