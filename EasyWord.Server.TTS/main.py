import torch
import scipy
from transformers import AutoProcessor
# from IPython.display import Audio
from transformers import BarkModel
from transformers import set_seed

processor = AutoProcessor.from_pretrained("D://VSCodeField//1models//bark-small")

device = "cuda:0" if torch.cuda.is_available() else "cpu"
print(device)

#输入
text_prompt = "generating speech from text is a fun task"
inputs = processor(text_prompt).to(device)

#加载模型
model = BarkModel.from_pretrained("D://VSCodeField//1models//bark-small").to(device)

# convert to bettertransformer
model = model.to_bettertransformer()


def measure_latency_and_memory_use(model, inputs, nb_loops = 10):

  # define Events that measure start and end of the generate pass
  start_event = torch.cuda.Event(enable_timing=True)
  end_event = torch.cuda.Event(enable_timing=True)

  # reset cuda memory stats and empty cache
  torch.cuda.reset_peak_memory_stats(device)
  torch.cuda.empty_cache()
  torch.cuda.synchronize()

  # get the start time
  start_event.record()

  # actually generate
  for _ in range(nb_loops):
        # set seed for reproducibility
        set_seed(0)
        output = model.generate(**inputs, do_sample = True, fine_temperature = 0.4, coarse_temperature = 0.8)

  # get the end time
  end_event.record()
  torch.cuda.synchronize()

  # measure memory footprint and elapsed time
  max_memory = torch.cuda.max_memory_allocated(device)
  elapsed_time = start_event.elapsed_time(end_event)* 1.0e-3

  print('Execution time:', elapsed_time/nb_loops, 'seconds')
  print('Max memory footprint', max_memory*1e-9, ' GB')

  return output


with torch.inference_mode():
  speech_output = measure_latency_and_memory_use(model, inputs, nb_loops = 1)


# #不知道为什么不能直接播放
# # now, listen to the output
# sampling_rate = model.generation_config.sample_rate
# Audio(speech_output[0].cpu().numpy(), rate=sampling_rate, autoplay=True)

#存储为.wav文件
sampling_rate = model.generation_config.sample_rate
print(sampling_rate)
scipy.io.wavfile.write("bark_out.wav", rate=sampling_rate, data=speech_output.cpu().numpy().squeeze())