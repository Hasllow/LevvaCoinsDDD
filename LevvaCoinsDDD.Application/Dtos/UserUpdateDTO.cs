﻿namespace LevvaCoinsDDD.Application.Dtos;
public class UserUpdateDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string AvatarUrl { get; set; }
}
