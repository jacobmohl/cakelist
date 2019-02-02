using System;
using System.Collections.Generic;
using System.Text;

namespace Cakelist.Business.Interfaces
{
    interface ICakelistService
    {
        void GetCakelist();
        void AddCakeRequest();
        void VoteOnCakeRequest();
    }
}
