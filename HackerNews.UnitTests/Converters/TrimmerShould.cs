//using HackerNews.Api.Converters;
//using HackerNews.Api.Helpers.Objects;
//using HackerNews.Domain;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Xunit;

//namespace HackerNews.UnitTests.Converters
//{
//	public class TrimmerShould
//	{
//		[Fact]
//		public void GetNewTrimmedArticle_WithChildren_NotMutateOriginal_ReturnsNewWithChildren()
//		{
//			var _comment = new Comment();
//			var _article = new Article();

//			_article.Comments.Add(_comment);
//			_comment.ParentArticle = _article;

//			// Act
//			var newArticle = Trimmer.GetNewTrimmedArticle(_article, false);

//			// Assert
//			// OG and new article should still contain children
//			Assert.Contains(_comment, _article.Comments);
//			Assert.NotEmpty(newArticle.Comments);

//			// new children shouldn't refernece any parent (no circular references)
//			Assert.Null(newArticle.Comments.FirstOrDefault().ParentArticle);
//			Assert.Null(newArticle.Comments.FirstOrDefault().ParentComment);
//		}

//		[Fact]
//		public void GetNewTrimmedArticle_WithoutChildren_NotMutateOriginal_ReturnsNewWithoutChildren()
//		{
//			var _comment = new Comment();
//			var _article = new Article();

//			_article.Comments.Add(_comment);
//			_comment.ParentArticle = _article;

//			// Act
//			var newArticle = Trimmer.GetNewTrimmedArticle(_article, true);

//			// Assert
//			// OG and new article should still contain children
//			Assert.Contains(_comment, _article.Comments);
//			Assert.True(_article.Comments.Count >= 1);

//			// new article shouldn't contain children
//			Assert.Empty(newArticle.Comments);
//		}

//		[Fact]
//		public async Task GetNewTrimmedArticlesAsync_WithChildren_NotMutateOriginal_ReturnsNewWithChildren()
//		{
//			var _comment = new Comment();
//			var _article = new Article();

//			var articles = new List<Article>();
//			var comments = new List<Comment>();
//			for (int i = 0; i < 3; i++)
//			{
//				var newArticle = _article.Copy();
//				var newComment = _comment.Copy();
//				newArticle.Comments.Add(newComment);
//				newComment.ParentArticle = newArticle;

//				articles.Add(newArticle);
//				comments.Add(newComment);
//			}

//			// Act
//			var newArticles = await Trimmer.GetNewTrimmedArticlesAsync(articles, false);

//			// Assert
//			// OG articles still have children
//			foreach (var article in articles) Assert.True(article.Comments.Count >= 1);

//			// OG comments still have a parent article
//			foreach (var comment in comments) Assert.NotNull(comment.ParentArticle);

//			foreach (var newArticle in newArticles)
//			{
//				// new articles still have children
//				Assert.True(newArticle.Comments.Count >= 1);

//				// new comments don't have parent article
//				foreach (var newComment in newArticle.Comments) Assert.Null(newComment.ParentArticle);
//			}
//		}

//		[Fact]
//		public async Task GetNewTrimmedArticlesAsync_WithoutChildren_NotMutateOriginal_ReturnsNewWithoutChildren()
//		{
//			var _comment = new Comment();
//			var _article = new Article();

//			var articles = new List<Article>();
//			var comments = new List<Comment>();
//			for (int i = 0; i < 3; i++)
//			{
//				var newArticle = _article.Copy();
//				var newComment = _comment.Copy();
//				newArticle.Comments.Add(newComment);
//				newComment.ParentArticle = newArticle;

//				articles.Add(newArticle);
//				comments.Add(newComment);
//			}

//			// Act
//			var newArticles = await Trimmer.GetNewTrimmedArticlesAsync(articles, true);

//			// Assert
//			// OG articles still have children
//			foreach (var article in articles) Assert.True(article.Comments.Count >= 1);

//			// OG comments still have a parent article
//			foreach (var comment in comments) Assert.NotNull(comment.ParentArticle);

//			foreach (var newArticle in newArticles)
//			{
//				// new articles don't have children
//				Assert.Empty(newArticle.Comments);
//			}
//		}

//		[Fact]
//		public void GetNewTrimmedComment_WithParents_WithChildren_NotMutateOriginal_ReturnsNewWithParentsWithChildren()
//		{
//			var _comment = new Comment();
//			var _parentComment = new Comment();
//			var _parentArticle = new Article();

//			_parentArticle.Comments.Add(_comment);
//			_parentComment.Comments.Add(_comment);
//			_comment.ParentArticle = _parentArticle;
//			_comment.ParentComment = _parentComment;

//			// Act
//			var newComment = Trimmer.GetNewTrimmedComment(_comment, trimParents: false, trimChildren: false);

//			// Assert
//			// OG and new comments should still contain parents
//			Assert.Equal(_parentArticle, _comment.ParentArticle);
//			Assert.Equal(_parentComment, _comment.ParentComment);
//			Assert.NotNull(newComment.ParentArticle);
//			Assert.NotNull(newComment.ParentComment);

//			// OG parents should contain OG comment
//			Assert.Contains(_comment, _parentComment.Comments);
//			Assert.Contains(_comment, _parentArticle.Comments);

//			// new parents shouldn't refernece any children (no circular references)
//			Assert.Empty(newComment.ParentArticle.Comments);
//			Assert.Empty(newComment.ParentComment.Comments);
//		}

//		[Fact]
//		public void GetNewTrimmedComment_WithoutParents_WithoutChildren_NotMutateOriginal_ReturnsNewWithoutChildrenWithoutParents()
//		{
//			var _comment = new Comment();
//			var _parentComment = new Comment();
//			var _parentArticle = new Article();

//			_parentArticle.Comments.Add(_comment);
//			_parentComment.Comments.Add(_comment);
//			_comment.ParentArticle = _parentArticle;
//			_comment.ParentComment = _parentComment;

//			// Act
//			var newComment = Trimmer.GetNewTrimmedComment(_comment, trimParents: true, trimChildren: true);

//			// Assert
//			// OG comments should still contain parents
//			Assert.Equal(_parentArticle, _comment.ParentArticle);
//			Assert.Equal(_parentComment, _comment.ParentComment);

//			// OG parents should contain OG comment
//			Assert.Contains(_comment, _parentComment.Comments);
//			Assert.Contains(_comment, _parentArticle.Comments);


//			// new comments shouldn't have parents
//			Assert.Null(newComment.ParentArticle);
//			Assert.Null(newComment.ParentComment);
//		}

//		[Fact]
//		public async Task GetNewTrimmedComments_WithParents_WithChildren_NotMutateOriginals_ReturnsNewWithParentsWithChildren()
//		{
//			var articles = new List<Article>();
//			var comments = new List<Comment>();
//			for (int i = 0; i < 3; i++)
//			{
//				var newArticle = new Article();
//				var newComment = new Comment();
//				var parentComment = new Comment();

//				newArticle.Comments.Add(newComment);
//				parentComment.Comments.Add(newComment);
//				newComment.ParentArticle = newArticle;
//				newComment.ParentComment = parentComment;

//				articles.Add(newArticle);
//				comments.Add(newComment);
//			}

//			// Act
//			var newComments = await Trimmer.GetNewTrimmedCommentsAsync(comments, trimParents: false, trimChildren: false);

//			// Assert
//			// OG articles still have children
//			foreach (var article in articles) Assert.NotEmpty(article.Comments);

//			// OG comments still have a parents
//			foreach (var comment in comments) Assert.NotNull(comment.ParentArticle);

//			foreach (var newComment in newComments)
//			{
//				// new comments still have parents
//				Assert.NotNull(newComment.ParentArticle);
//				Assert.NotNull(newComment.ParentComment);

//				// new parents don't have children (circ ref)
//				Assert.Empty(newComment.ParentArticle.Comments);
//				Assert.Empty(newComment.ParentComment.Comments);
//			}
//		}
//	}
//}
