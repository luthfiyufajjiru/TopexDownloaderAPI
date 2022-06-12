namespace TopexDownloaderAPI.Services.ViewText
{
    public static class TemplateText
    {
        public static string ErrorAreaTooBig()
        {
            return 
            @"<style>
                .oaerror {
                    width: 80%;
                    background-color: #ffffff;
                    padding: 20px;
                    border: 1px solid #eee;
                    border-left-width: 5px;
                    border-radius: 3px;
                    margin: 10px auto;
                    font-family: 'Open Sans', sans-serif;
                    font-size: 16px;
                }

                .danger {
                    border-left-color: #d9534f;
                    background-color: rgba(217, 83, 79, 0.1);
                }

                .danger strong {
                    color: #d9534f;
                }
            </style>

            <div class=""oaerror danger"">
                <strong> Application Error </strong> - The area is way too big! Please try again.
            </div> ";
        }
    }
}
