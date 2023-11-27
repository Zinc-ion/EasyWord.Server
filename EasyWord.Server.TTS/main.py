import torch
import scipy
from transformers import AutoProcessor
# from IPython.display import Audio
from transformers import BarkModel

processor = AutoProcessor.from_pretrained("D://VSCodeField//1models//bark-small")

device = "cuda:0" if torch.cuda.is_available() else "cpu"

#输入
text_prompt = "Let's try generating speech, with Bark, a text-to-speech model"
inputs = processor(text_prompt).to(device)

#加载模型
model = BarkModel.from_pretrained("D://VSCodeField//1models//bark-small").to(device)

# convert to bettertransformer
model =  model.to_bettertransformer()


with torch.inference_mode():
  speech_output = model.generate(**inputs, do_sample = True, fine_temperature = 0.4, coarse_temperature = 0.8)

# 不知道为什么不能直接播放
# # now, listen to the output
# sampling_rate = model.generation_config.sample_rate
# Audio(speech_output[0].cpu().numpy(), rate=sampling_rate, autoplay=True)

#存储为.wav文件
sampling_rate = model.generation_config.sample_rate
print(sampling_rate)
scipy.io.wavfile.write("bark_out.wav", rate=sampling_rate, data=speech_output.cpu().numpy().squeeze())