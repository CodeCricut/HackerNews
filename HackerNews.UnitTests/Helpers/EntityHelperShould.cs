using AutoMapper;
using HackerNews.Api.Helpers;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using HackerNews.EF.Repositories;
using Moq;
using System.Collections.Generic;

namespace HackerNews.UnitTests.Helpers
{
	public abstract class EntityHelperShould
	{
		public Article _article;
		public List<Article> _articles;

		public Comment _comment;
		public List<Comment> _comments;

		public PostArticleModel _postArticleModel;
		public List<PostArticleModel> _postArticleModels;

		public PostCommentModel _postCommentModel;
		public List<PostCommentModel> _postCommentModels;

		public GetArticleModel _getArticleModel;
		public List<GetArticleModel> _getArticleModels;

		public GetCommentModel _getCommentModel;
		public List<GetCommentModel> _getCommentModels;

		public Mock<IEntityRepository<Article>> _mockArticleRepository;
		public Mock<IEntityRepository<Comment>> _mockCommentRepository;

		public Mock<IMapper> _mockMapper;

		public ArticleHelper _articleHelper;
		public CommentHelper _commentHelper;

		public EntityHelperShould()
		{
			SetupArticleObjects();
			SetupCommentObjects();

			_mockArticleRepository = new Mock<IEntityRepository<Article>>();
			_mockCommentRepository = new Mock<IEntityRepository<Comment>>();

			_mockMapper = new Mock<IMapper>();

			_articleHelper = new ArticleHelper(_mockArticleRepository.Object, _mockMapper.Object);
			_commentHelper = new CommentHelper(_mockCommentRepository.Object, _mockMapper.Object);

			SetupMockArticleRepo();
			SetupMockCommentRepo();
			SetupMockMapper();
		}

		private void SetupMockMapper()
		{
			_mockMapper.Setup(m => m.Map<Article>(It.IsAny<Article>())).Returns(_article);
			_mockMapper.Setup(m => m.Map<Article>(It.IsAny<PostArticleModel>())).Returns(_article);
			_mockMapper.Setup(m => m.Map<GetArticleModel>(It.IsAny<Article>())).Returns(_getArticleModel);

			_mockMapper.Setup(m => m.Map<Comment>(It.IsAny<Comment>())).Returns(_comment);
			_mockMapper.Setup(m => m.Map<Comment>(It.IsAny<PostCommentModel>())).Returns(_comment);
			_mockMapper.Setup(m => m.Map<GetCommentModel>(It.IsAny<Comment>())).Returns(_getCommentModel);

			_mockMapper.Setup(m => m.Map<List<Article>>(It.IsAny<List<Article>>())).Returns(_articles);
			_mockMapper.Setup(m => m.Map<List<Article>>(It.IsAny<List<PostArticleModel>>())).Returns(_articles);
			_mockMapper.Setup(m => m.Map<List<GetArticleModel>>(It.IsAny<List<Article>>())).Returns(_getArticleModels);

			_mockMapper.Setup(m => m.Map<List<Comment>>(It.IsAny<List<Comment>>())).Returns(_comments);
			_mockMapper.Setup(m => m.Map<List<Comment>>(It.IsAny<List<PostCommentModel>>())).Returns(_comments);
			_mockMapper.Setup(m => m.Map<List<GetCommentModel>>(It.IsAny<List<Comment>>())).Returns(_getCommentModels);
		}

		private void SetupMockCommentRepo()
		{
			_mockCommentRepository.Setup(r => r.GetEntityAsync(It.IsAny<int>())).ReturnsAsync(_comment);
			_mockCommentRepository.Setup(r => r.GetEntitiesAsync()).ReturnsAsync(_comments);
		}

		private void SetupMockArticleRepo()
		{
			_mockArticleRepository.Setup(r => r.GetEntityAsync(It.IsAny<int>())).ReturnsAsync(_article);
			_mockArticleRepository.Setup(r => r.GetEntitiesAsync()).ReturnsAsync(_articles);
		}

		private void SetupCommentObjects()
		{
			_comment = new Comment
			{
				AuthorName = "Author name",
				Text = "text",
				Url = "url"
			};
			_comments = ListHelper.CreateListOfClones(_comment, 5);

			_postCommentModel = new PostCommentModel();
			_postCommentModels = ListHelper.CreateListOfType<PostCommentModel>(5);

			_getCommentModel = new GetCommentModel();
			_getCommentModels = ListHelper.CreateListOfType<GetCommentModel>(5);
		}

		private void SetupArticleObjects()
		{
			_article = new Article
			{
				AuthorName = "author name",
				Id = 0,
				Text = "text",
				Title = "title",
				Url = "url",
				Type = ArticleType.Meta
			};
			_articles = ListHelper.CreateListOfClones(_article, 5);

			_postArticleModel = new PostArticleModel();
			_postArticleModels = ListHelper.CreateListOfType<PostArticleModel>(5);

			_getArticleModel = new GetArticleModel();
			_getArticleModels = ListHelper.CreateListOfType<GetArticleModel>(5);
		}
	}
}
