﻿using Microsoft.AspNetCore.Mvc;
using TaskoMask.BuildingBlocks.Contracts.Dtos.Cards;
using TaskoMask.BuildingBlocks.Web.MVC.Controllers;
using TaskoMask.BuildingBlocks.Contracts.Helpers;
using TaskoMask.BuildingBlocks.Contracts.Services;
using TaskoMask.BuildingBlocks.Application.Bus;
using TaskoMask.Services.Boards.Write.Application.UseCases.Cards.AddCard;
using TaskoMask.Services.Boards.Write.Application.UseCases.Cards.UpdateCard;
using TaskoMask.Services.Boards.Write.Application.UseCases.Cards.DeleteCard;
using Microsoft.AspNetCore.Authorization;

namespace TaskoMask.Services.Cards.Write.Api.Controllers
{
    [Authorize("user-write-access")]
    public class CardsController : BaseApiController
    {
        #region Fields


        #endregion

        #region Ctors

        public CardsController(IAuthenticatedUserService authenticatedUserService, IInMemoryBus inMemoryBus) : base(authenticatedUserService, inMemoryBus)
        {
        }


        #endregion

        #region Public Methods



        /// <summary>
        /// Add new card
        /// </summary>
        [HttpPost]
        [Route("cards")]
        public async Task<Result<CommandResult>> Add([FromBody] AddCardDto input)
        {
            return await _inMemoryBus.SendCommand<AddCardRequest>(new(boardId: input.BoardId, name: input.Name, type: input.Type));
        }



        #endregion

    }
}
