using DataAccessLayer;
using DataModels;
using Interfaces;
using LinqToDB;

public class ModeratorService : IModeratorService
{
    private readonly DataBaseConnection connection;

    public ModeratorService(DataBaseConnection connection)
    {
        this.connection = connection;
    }

    public async Task<RequestWrapper> ToggleContentFlag(ContentToggle request)
    {
        try 
        {
            var response = new RequestWrapper();
            switch (request.ContentType)
            {
                case ContentTypeEnum.Post:
                    return await TogglePostFlags(request);
                case ContentTypeEnum.Comment:
                    return await ToggleCommentFlags(request);
                default:
                    response.Message = "Content type not found";
                    return response;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var response = new RequestWrapper();
            return response;
        }
    }

    private async Task<RequestWrapper> TogglePostFlags(ContentToggle request)
    {
        var response = new RequestWrapper();
        var post = await connection.GetTable<PostModel>().FirstOrDefaultAsync(x => x.Id == request.ContentId);
        if (post == null)
        {
            response.Message = "Post not found";
            return response;
        }
        //SQLite doesn't have a boolean type so we use 1 for true and 0 for false
        post.IsFalseInformation = request.IsFalseInformation ? 1 : 0;
        post.IsMisleading = request.IsMisleading ? 1 : 0;
        await connection.UpdateAsync(post);
        response.SetSucceeded();
        return response;
    }

    private async Task<RequestWrapper> ToggleCommentFlags(ContentToggle request)
    {
        var response = new RequestWrapper();
        var comment = await connection.GetTable<CommentModel>().FirstOrDefaultAsync(x => x.Id == request.ContentId);
        if (comment == null)
        {
            response.Message = "Comment not found";
            return response;
        }
        comment.IsFalseInformation = request.IsFalseInformation ? 1 : 0;
        comment.IsMisleading = request.IsMisleading ? 1 : 0;
        await connection.UpdateAsync(comment);
        response.SetSucceeded();
        return response;
    }
}