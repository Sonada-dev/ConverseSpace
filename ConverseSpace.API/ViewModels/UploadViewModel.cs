using System.ComponentModel.DataAnnotations;
using ConverseSpace.Application.Posts.Commands;
using Microsoft.AspNetCore.Mvc;

namespace ConverseSpace.API.ViewModels;

public class UploadViewModel
{
    [Required]
    public required CreatePostCommand Command { get; set; }
    public IFormFileCollection? Files { get; set; }
}