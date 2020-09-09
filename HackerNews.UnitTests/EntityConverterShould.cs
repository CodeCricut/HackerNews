using AutoMapper;
using HackerNews.Api.Converters;
using HackerNews.Api.Helpers;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using HackerNews.EF;
using Moq;
using System;
using System.Collections.Generic;

namespace HackerNews.UnitTests
{
	public abstract class EntityConverterShould
	{
		public Article _article;
		public Comment _comment;

		public Article _parentArticle;
		public Comment _parentComment;

		public List<Article> _articles;
		public List<Comment> _comments;

		public PostArticleModel _postArticleModel;
		public List<PostArticleModel> _postArticleModels;

		public PostCommentModel _postCommentModel;
		public List<PostCommentModel> _postCommentModels;

		public Mock<IMapper> _mockMapper;

		public Mock<EntityRepository<Comment>> _mockCommentRepo;
		public Mock<EntityRepository<Article>> _mockArticleRepo;

		public CommentConverter _commentConverter;
		public ArticleConverter _articleConverter;

		public EntityConverterShould()
		{
			_article = new Article();
			_comment = new Comment();

			_parentArticle = new Article();
			_parentComment = new Comment();

			_articles = ListHelper.CreateListOfType<Article>(5);
			_comments = ListHelper.CreateListOfType<Comment>(5);

			_postArticleModel = new PostArticleModel();
			_postArticleModels = ListHelper.CreateListOfType<PostArticleModel>(5);

			_postCommentModel = new PostCommentModel();
			_postCommentModels = ListHelper.CreateListOfType<PostCommentModel>(5);

			_mockMapper = new Mock<IMapper>();

			_mockCommentRepo = new Mock<EntityRepository<Comment>>(null);
			_mockArticleRepo = new Mock<EntityRepository<Article>>(null);

			_commentConverter = new CommentConverter(_mockMapper.Object, _mockCommentRepo.Object, _mockArticleRepo.Object);
			_articleConverter = new ArticleConverter(_mockMapper.Object);

			SetupDefaultMockMapperBehavior(_mockMapper);
		}

		

		private static void SetupDefaultMockMapperBehavior(Mock<IMapper> mockMapper)
		{
			mockMapper.Setup(m => m.Map<Article>(It.IsAny<PostArticleModel>())).Returns(new Article());
			mockMapper.Setup(m => m.Map<Comment>(It.IsAny<PostCommentModel>())).Returns(new Comment());

			mockMapper.Setup(m => m.Map<GetCommentModel>(It.IsAny<Comment>())).Returns(new GetCommentModel());
			mockMapper.Setup(m => m.Map<GetArticleModel>(It.IsAny<Article>())).Returns(new GetArticleModel());
		}
	}
}
