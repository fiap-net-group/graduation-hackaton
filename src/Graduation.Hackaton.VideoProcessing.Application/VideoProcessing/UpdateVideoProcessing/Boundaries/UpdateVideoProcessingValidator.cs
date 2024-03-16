using FluentValidation;
using Graduation.Hackaton.VideoProcessing.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.UpdateVideoProcessing.Boundaries
{
    public class UpdateVideoProcessingValidator : AbstractValidator<UpdateVideoProcessingInput>
    {
        public UpdateVideoProcessingValidator() 
        {
            RuleFor(req => req.value.Id).NotEmpty().WithMessage(ResponseMessage.InvalidId.ToString());
            RuleFor(req => req.value.ImagesPath).NotEmpty().WithMessage(ResponseMessage.InvalidStatus.ToString());
            RuleFor(req => req.value.Status).NotEmpty().WithMessage(ResponseMessage.InvalidStatus.ToString());
            RuleFor(req => req.value.UpdatedAt).NotEmpty();
            
        }
    }
}
