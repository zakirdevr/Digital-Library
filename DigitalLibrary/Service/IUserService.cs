﻿namespace DigitalLibrary.Service
{
    public interface IUserService
    {
        string GetUserId();
        bool IsAuthenticated();
    }
}