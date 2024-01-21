using DataAccessLayer;
using DataModels;
using Interfaces;
using LinqToDB;

public class PostService : IPostService
{
    private readonly DataBaseConnection connection;

    public PostService(DataBaseConnection connection)
    {
        this.connection = connection;
    }

    private static int PageSize = 20;

    public async Task<RequestWrapper<List<Post>>> GetPosts(int currentPage)
    {
        try
        {
            var request = new RequestWrapper<List<Post>>();
            var posts = await connection.GetTable<PostModel>().Skip((currentPage - 1) * PageSize).Take(PageSize).ToListAsync();
            request.Data = posts.Select(x => x.MapToPost()).ToList();
            request.SetSucceeded();
            return request;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new RequestWrapper<List<Post>>();
        }
    }

    public async Task<RequestWrapper<Post>> GetPost(int id)
    {
        try
        {
            var request = new RequestWrapper<Post>();
            var post = await connection.GetTable<PostModel>().FirstOrDefaultAsync(x => x.Id == id);
            if (post == null)
            {
                request.Message = "Post not found";
                return request;
            }
            request.Data = post.MapToPost();
            return request.SetSucceeded();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new RequestWrapper<Post>();
        }
    }

    public async Task<RequestWrapper> CreatePost(Post post, UserModel userModel)
    {
        try
        {
            var request = new RequestWrapper();
            var postModel = new PostModel(post.Title, post.Content, userModel.Id);
            await connection.InsertAsync(postModel);
            return request.SetSucceeded();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new RequestWrapper();
        }
    }

    public async Task<RequestWrapper> LikePost(int id, UserModel userModel)
    {
        try
        {
            var request = new RequestWrapper();
            var post = await connection.GetTable<PostModel>().FirstOrDefaultAsync(x => x.Id == id);
            var like = await connection.GetTable<LikeModel>().FirstOrDefaultAsync(x => x.PostId == id && x.UserId == userModel.Id); 

            if (post == null)
            {
                return request.SetFailed("Post not found");
            }

            if(like != null || post.CreatedByUserID == userModel.Id)
            {
                // The message expands on condition 1, condition 2 however is not mentioned if the user is trying to like a post that they created.
                return request.SetFailed("Post already liked");
            }

            post.LikeCount++;
            await connection.UpdateAsync(post);
            await connection.InsertAsync(new LikeModel()
            {
                PostId = id,
                UserId = userModel.Id,
            });
            request.SetSucceeded();
            return request;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new RequestWrapper();
        }
    }

    public async Task<RequestWrapper> UnlikePost(int id, UserModel userModel)
    {
        try
        {
            var request = new RequestWrapper();
            var post = await connection.GetTable<PostModel>().FirstOrDefaultAsync(x => x.Id == id);
            var like = await connection.GetTable<LikeModel>().FirstOrDefaultAsync(x => x.PostId == id && x.UserId == userModel.Id); 
            if (post == null)
            {
                request.Message = "Post not found";
                return request;
            }
            if (like == null || post.CreatedByUserID == userModel.Id)
            {
                // The message expands on condition 1, condition 2 however is not mentioned if the user is trying to unlike a post that they created.
                request.Message = "Post not liked cant unlike";
                return request;
            }

            post.LikeCount--;
            await connection.UpdateAsync(post);
            await connection.DeleteAsync(like); //TODO: Introduce soft delete
            request.SetSucceeded();
            return request;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new RequestWrapper();
        }
    }

}