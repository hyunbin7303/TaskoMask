﻿using TaskoMask.Domain.Core.Models;
using System.Collections.Generic;
using System.Linq;
using TaskoMask.Domain.Share.Resources;
using TaskoMask.Domain.Core.Exceptions;
using TaskoMask.Domain.Workspace.Boards.Events.Boards;
using TaskoMask.Domain.Workspace.Boards.ValueObjects.Boards;
using TaskoMask.Domain.Workspace.Boards.Events.Members;
using TaskoMask.Domain.Workspace.Boards.Events.Cards;
using TaskoMask.Domain.Share.Enums;

namespace TaskoMask.Domain.Workspace.Boards.Entities
{
    public class Board: AggregateRoot
    {
        #region Fields


        #endregion

        #region Ctors

        private Board(string name, string description, string projectId)
        {
            Name = BoardName.Create(name);
            Description = BoardDescription.Create(description);
            ProjectId = BoardProjectId.Create(projectId);

            AddDomainEvent(new BoardCreatedEvent(Id, Name.Value, Description.Value, ProjectId.Value));
        }

        #endregion

        #region Properties

        public BoardName Name { get; private set; }
        public BoardDescription Description { get; private set; }
        public BoardProjectId ProjectId { get; private set; }
        public ICollection<Card> Cards { get; private set; }
        public ICollection<Member> Members { get; private set; }


        #endregion

        #region Public Board Methods




        /// <summary>
        /// 
        /// </summary>
        public static Board CreateBoard(string name, string description, string projectId)
        {
            return new Board(name, description, projectId);
        }



        /// <summary>
        /// 
        /// </summary>
        public void UpdateBoard(string name, string description )
        {
            Name = BoardName.Create(name);
            Description = BoardDescription.Create(description);

            base.UpdateModifiedDateTime();

            CheckPolicies();

            AddDomainEvent(new BoardUpdatedEvent(Id, Name.Value, Description.Value));

        }



        /// <summary>
        /// 
        /// </summary>
        public void DeleteBoard()
        {
            base.Delete();
            AddDomainEvent(new BoardDeletedEvent(Id));
        }



        /// <summary>
        /// 
        /// </summary>
        public void RecycleBoard()
        {
            base.Recycle();
            AddDomainEvent(new BoardRecycledEvent(Id));
        }



        #endregion

        #region Public Card Methods



        /// <summary>
        /// 
        /// </summary>
        public void CreateCard(Card card)
        {
            Cards.Add(card);
            AddDomainEvent(new CardCreatedEvent(card.Id, card.Name.Value,card.Type.Value, Id));
        }



        /// <summary>
        /// 
        /// </summary>
        public void UpdateCard(string id, string name, BoardCardType type)
        {
            var card = Cards.FirstOrDefault(p => p.Id == id);
            if (card == null)
                throw new DomainException(string.Format(DomainMessages.Not_Found, DomainMetadata.Card));

            card.Update(name, type);

            AddDomainEvent(new CardUpdatedEvent(card.Id, card.Name.Value, card.Type.Value));
        }



        /// <summary>
        /// 
        /// </summary>
        public void DeleteCard(string id)
        {
            var card = Cards.FirstOrDefault(p => p.Id == id);
            if (card == null)
                throw new DomainException(string.Format(DomainMessages.Not_Found, DomainMetadata.Card));

            card.Delete();
            AddDomainEvent(new CardDeletedEvent(card.Id));
        }



        /// <summary>
        /// 
        /// </summary>
        public void RecycleCard(string id)
        {
            var card = Cards.FirstOrDefault(p => p.Id == id);
            if (card == null)
                throw new DomainException(string.Format(DomainMessages.Not_Found, DomainMetadata.Card));

            card.Recycle();
            AddDomainEvent(new CardRecycledEvent(card.Id));
        }




        #endregion

        #region Public Member Methods



        /// <summary>
        /// 
        /// </summary>
        public void CreateMember(Member member)
        {
            Members.Add(member);
            AddDomainEvent(new MemberCreatedEvent(member.Id, member.OwnerId.Value, member.AccessLevel.Value, Id));
        }



        /// <summary>
        /// 
        /// </summary>
        public void UpdateMember(string id, BoardMemberAccessLevel accessLevel )
        {
            var member = Members.FirstOrDefault(p => p.Id == id);
            if (member == null)
                throw new DomainException(string.Format(DomainMessages.Not_Found, DomainMetadata.Member));

            member.Update(accessLevel);

            AddDomainEvent(new MemberUpdatedEvent(member.Id, member.AccessLevel.Value));
        }



        /// <summary>
        /// 
        /// </summary>
        public void DeleteMember(string id)
        {
            var member = Members.FirstOrDefault(p => p.Id == id);
            if (member == null)
                throw new DomainException(string.Format(DomainMessages.Not_Found, DomainMetadata.Member));

            member.Delete();
            AddDomainEvent(new MemberDeletedEvent(member.Id));
        }




        #endregion

        #region Private Methods



        /// <summary>
        /// 
        /// </summary>
        private void CheckPolicies()
        {

        }



        /// <summary>
        /// 
        /// </summary>
        protected override void CheckInvariants()
        {

        }



        #endregion
    }
}
