using System;
using System.Text.RegularExpressions;
using Paramore.Rewind.Core.Domain.Common;

namespace Paramore.Rewind.Core.Domain.Venues
{
    public class Address : IEquatable<Address>, IFormattable, IAmAValueType<string>
    {
        private readonly City city;
        private readonly PostCode postalCode;
        private readonly Street street;

        public Address(Street street, City city, PostCode postalCode)
        {
            this.street = street;
            this.postalCode = postalCode;
            this.city = city;
        }

        public Address()
        {
            city = new City();
            postalCode = new PostCode();
            street = new Street();
        }

        public string Value
        {
            get { return ToString(); }
        }

        public bool Equals(Address rhs)
        {
            if (ReferenceEquals(null, rhs)) return false;
            if (ReferenceEquals(this, rhs)) return true;
            return Equals(rhs.city, city) && Equals(rhs.postalCode, postalCode) && Equals(rhs.street, street);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case "G":
                default:
                    return ToString();
            }
        }

        public static implicit operator string(Address rhs)
        {
            return rhs.ToString();
        }

        public override string ToString()
        {
            return string.Format("Street: {0}, City: {1}, PostCode: {2}", street, city, postalCode);
        }

        public static Address Parse(string address)
        {
            var rx = new Regex("BuildingNumber: (.*), StreetName: (.*), City: (.*), PostCode: (.*)");
            Match match = rx.Match(address);

            string streetNumber = match.Groups[1].Value;
            int number;
            if (!int.TryParse(streetNumber, out number)) number = 0;
            string streetName = match.Groups[2].Value;
            var street = new Street(number, streetName);

            var city = new City(match.Groups[3].Value);
            var postcode = new PostCode(match.Groups[4].Value);
            return new Address(street, city, postcode);
        }

        public override bool Equals(object rhs)
        {
            if (ReferenceEquals(null, rhs)) return false;
            if (ReferenceEquals(this, rhs)) return true;
            if (rhs.GetType() != typeof (Address)) return false;
            return Equals((Address) rhs);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (city != null ? city.GetHashCode() : 0);
                result = (result*397) ^ (postalCode != null ? postalCode.GetHashCode() : 0);
                result = (result*397) ^ (street != null ? street.GetHashCode() : 0);
                return result;
            }
        }

        public static bool operator ==(Address left, Address right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Address left, Address right)
        {
            return !Equals(left, right);
        }
    }
}