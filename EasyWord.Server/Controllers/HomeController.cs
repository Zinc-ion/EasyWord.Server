﻿using EasyWord.Server.Services;
using EasyWord.Server.Commands;
using Microsoft.AspNetCore.Mvc;
using TheSalLab.GeneralReturnValues;

namespace EasyWord.Server.Controllers;

[ApiController]
[Route("[controller]")]
// /Home
public class HomeController {
    private ISentenceComposingService _sentenceComposingService;
    private ITextComposingService _textComposingService;
    private IImage2WordService _image2WordService;
    private IText2ImageService _text2ImageService;

    public HomeController(ISentenceComposingService sentenceComposingService, ITextComposingService textComposingService
        , IImage2WordService image2WordService, IText2ImageService text2ImageService)
    {
        _sentenceComposingService = sentenceComposingService;
        _textComposingService = textComposingService;
        _image2WordService = image2WordService;
        _text2ImageService = text2ImageService;
    }


    [HttpPost]
    [Route("sentenceComposing")]
    public async Task<ServiceResultViewModel<string>> SentenceComposingAsync(
        [FromForm] SentenceCommand command)
    {
        string sentence;
        try
        {
            sentence = await _sentenceComposingService.ComposeAsync(command.Word);
        }
        catch (Exception ex)
        {
            return ServiceResult<string>.CreateExceptionResult(ex, ex.Message).ToServiceResultViewModel();
        }
        return ServiceResult<string>.CreateSucceededResult(sentence).ToServiceResultViewModel();
    }

    [HttpPost]
    [Route("textComposing")]
    public async Task<ServiceResultViewModel<string>> TextComposingAsync(
        [FromForm] TextCommand command)
    {
        string text;
        try
        {
            text = await _textComposingService.ComposeTextAsync(command.words);
        }
        catch (Exception ex)
        {
            return ServiceResult<string>.CreateExceptionResult(ex, ex.Message).ToServiceResultViewModel();
        }
        return ServiceResult<string>.CreateSucceededResult(text).ToServiceResultViewModel();
    }

    [HttpPost]
    [Route("image2Word")]
    public async Task<ServiceResultViewModel<string>> Image2Word(
        [FromForm] ImageCommand command)
    {
        using var httpClient = new HttpClient();
        using var formData = new MultipartFormDataContent {
            {new StreamContent(command.File.OpenReadStream()), "file", "file"}
        };
        HttpResponseMessage response;
        try
        {
            response = await httpClient.PostAsync(
                "http://easyword.server.image2word:8000", formData);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            return ServiceResult<string>.CreateExceptionResult(e, e.Message)
                .ToServiceResultViewModel();
        }
        //图片描述结构
        var desc = await response.Content.ReadAsStringAsync();
        string descWithWordAndSentence;
        try
        {
            descWithWordAndSentence = await _image2WordService.ComposeAsync(desc);
        }
        catch (Exception e)
        {
            return ServiceResult<string>.CreateExceptionResult(e, e.Message)
                .ToServiceResultViewModel();
        }
        return ServiceResult<string>.CreateSucceededResult(descWithWordAndSentence)
            .ToServiceResultViewModel();
    }

    //根据例句生成图片
    [HttpPost]
    [Route("text2Image")]
    public async Task<string> Text2Image([FromQuery(Name = "command")] string command)
    {

        string data;
        try
        {
            data = await _text2ImageService.ComposeAsync(command);
        }
        catch (Exception ex)
        {
            return ServiceResult<string>.CreateExceptionResult(ex, ex.Message).ToServiceResultViewModel().ToString();
        }
        return data;

    }
}