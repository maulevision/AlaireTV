using System;
using System.Windows;

namespace AlaireTV
{
    public class SystemStatus
    {
        public string ConnectionStatus { get; set; } = "OK";
    public int CPUUsage { get; set; }
    public int GPUUsage { get; set; }
}
}