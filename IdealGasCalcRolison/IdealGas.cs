//TODO: -3 no comment header. RJG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idealGasCalculatorClass
{
    class IdealGas
    {

        //fields

        private double mass;
        private double volume;
        private double temp;
        private double molecularWeight;
        private double pressure;


        public double GetMass()
        {
            return mass;
        }

        public void SetMass(double newMass)
        {
            mass = newMass;
            Calc();
        }
        public double GetVolume()
        {
            return volume;
        }

        public void SetVolume(double newVolume)
        {
            volume = newVolume;
            Calc();
        }
        public double GetTemp()
        {
            return temp;
        }

        public void SetTemp(double newTemp)
        {
            temp = newTemp;
            Calc();
        }

        public double GetMolecularWeight()
        {
            return molecularWeight;
        }

        public void SetMolecularWeight(double newMol)
        {
            molecularWeight = newMol;
            Calc();
        }

        public double GetPressure()
        {
            return pressure;
        }

        private void Calc()
        {
            //since R is a constant, initialize as constant
            const double RCONSTANT = 8.3145;
            //get volume from user of container and calculate pressure/write to console
       
            pressure = (mass / molecularWeight) * RCONSTANT * (temp + 273.15) / volume;

           

        }

        public IdealGas()
        {

        }
    }
}
