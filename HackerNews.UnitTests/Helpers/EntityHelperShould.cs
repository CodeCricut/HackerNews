//using HackerNews.Api.Converters;
//using HackerNews.Api.Helpers;
//using HackerNews.Api.Helpers.EntityHelpers;
//using HackerNews.Domain;
//using HackerNews.Domain.Models;
//using HackerNews.EF.Repositories;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace HackerNews.UnitTests.Helpers
//{
//	public abstract class EntityHelperShould
//	{
//		public Mock<IEntityRepository<Article>> _mockArticleRepository;
//		//public Mock<IEntityConverter<Article, PostArticleModel, GetArticleModel>> _mockArticleConverter;

//		public ArticleHelper _articleHelper;

//		public Article _article;
//		public List<Article> _articles;

//		public PostArticleModel _postArticleModel;
//		public List<PostArticleModel> _postArticleModels;

//		public GetArticleModel _getArticleModel;
//		public List<GetArticleModel> _getArticleModels;


//		public Mock<IEntityRepository<Comment>> _mockCommentRepository;
//		//public Mock<IEntityConverter<Comment, PostCommentModel, GetCommentModel>> _mockCommentConverter;

//		public CommentHelper _commentHelper;

//		public Comment _comment;
//		public List<Comment> _comments;

//		public PostCommentModel _postCommentModel;
//		public List<PostCommentModel> _postCommentModels;

//		public GetCommentModel _getCommentModel;
//		public List<GetCommentModel> _getCommentModels;

//		public EntityHelperShould()
//		{
//			_mockArticleRepository = new Mock<IEntityRepository<Article>>();
//			//_mockArticleConverter = new Mock<IEntityConverter<Article, PostArticleModel, GetArticleModel>>();

//			_articleHelper = new ArticleHelper(_mockArticleRepository.Object, _mockArticleConverter.Object);

//			_article = new Article
//			{
//				AuthorName = "author name",
//				Id = 0,
//				Text = "text",
//				Title = "title",
//				Url = "url",
//				Type = ArticleType.Meta
//			};
//			_articles = ListHelper.CreateListOfClones(_article, 5);

//			_postArticleModel = new PostArticleModel();
//			_postArticleModels = ListHelper.CreateListOfType<PostArticleModel>(5);

//			_getArticleModel = new GetArticleModel();
//			_getArticleModels = ListHelper.CreateListOfType<GetArticleModel>(5);



//			_mockCommentRepository = new Mock<IEntityRepository<Comment>>();
//			//_mockCommentConverter = new Mock<IEntityConverter<Comment, PostCommentModel, GetCommentModel>>();

//			_commentHelper = new CommentHelper(_mockCommentRepository.Object, _mockCommentConverter.Object);

//			_comment = new Comment
//			{
//				AuthorName = "Author name",
//				Text = "text",
//				Url = "url"
//			};
//			_comments = ListHelper.CreateListOfClones(_comment, 5);

//			_postCommentModel = new PostCommentModel();
//			_postCommentModels = ListHelper.CreateListOfType<PostCommentModel>(5);

//			_getCommentModel = new GetCommentModel();
//			_getCommentModels = ListHelper.CreateListOfType<GetCommentModel>(5);
//		}
//	}
//}
