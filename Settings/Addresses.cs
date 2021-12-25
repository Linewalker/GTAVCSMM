using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAVCSMM.Settings
{
    class Addresses
    {
        private string _f_godm = "0x0";
        private string _f_nwanted = "0x0";
        private string _f_nragdoll = "0x0";
        private string _f_uoffradar = "0x0";
        private string _f_sbelt = "0x0";
        private string _f_sjump = "0x0";

        public string f_godm
        {
            get
            {
                return _f_godm;
            }
            set
            {
                _f_godm = value;
            }
        }
        public string f_nwanted
        {
            get
            {
                return _f_nwanted;
            }
            set
            {
                _f_nwanted = value;
            }
        }
        public string f_nragdoll
        {
            get
            {
                return _f_nragdoll;
            }
            set
            {
                _f_nragdoll = value;
            }
        }
        public string f_uoffradar
        {
            get
            {
                return _f_uoffradar;
            }
            set
            {
                _f_uoffradar = value;
            }
        }
        public string f_sbelt
        {
            get
            {
                return _f_sbelt;
            }
            set
            {
                _f_sbelt = value;
            }
        }
        public string f_sjump
        {
            get
            {
                return _f_sjump;
            }
            set
            {
                _f_sjump = value;
            }
        }
    }
}
