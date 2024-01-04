using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2.model
{
    public class Container
    {
        public Dictionary<int, Vehicle> VehicleDictonary { get; set; }
        public List<Rent> RentalDictonary { get; set; }

        public Container() 
        { 
            VehicleDictonary = new Dictionary<int, Vehicle>();
        }

        public void AddVehicle()
        {

        }

        public void RemoveVehicle()
        {

        }

        public void RentVehicle()
        {

        }

        public void ReadVehicleFile()
        {

        }

        public void ReadRentalFile()
        {

        }
    }
}
