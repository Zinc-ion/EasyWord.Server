
from transformers import VisionEncoderDecoderModel, ViTImageProcessor, AutoTokenizer
import torch
from PIL import Image

model = VisionEncoderDecoderModel.from_pretrained("D://VSCodeField//1models//models--nlpconnect--vit-gpt2-image-captioning//snapshots//dc68f91c06a1ba6f15268e5b9c13ae7a7c514084")
feature_extractor = ViTImageProcessor.from_pretrained("D://VSCodeField//1models//models--nlpconnect--vit-gpt2-image-captioning//snapshots//dc68f91c06a1ba6f15268e5b9c13ae7a7c514084")
tokenizer = AutoTokenizer.from_pretrained("D://VSCodeField//1models//models--nlpconnect--vit-gpt2-image-captioning//snapshots//dc68f91c06a1ba6f15268e5b9c13ae7a7c514084")

device = torch.device("cuda" if torch.cuda.is_available() else "cpu")
model.to(device)



max_length = 16
num_beams = 4
gen_kwargs = {"max_length": max_length, "num_beams": num_beams}
def predict_step(image_paths):
  images = []
  for image_path in image_paths:
    i_image = Image.open(image_path)
    if i_image.mode != "RGB":
      i_image = i_image.convert(mode="RGB")

    images.append(i_image)

  pixel_values = feature_extractor(images=images, return_tensors="pt").pixel_values
  pixel_values = pixel_values.to(device)

  output_ids = model.generate(pixel_values, **gen_kwargs)

  preds = tokenizer.batch_decode(output_ids, skip_special_tokens=True)
  preds = [pred.strip() for pred in preds]
  return preds


result = predict_step(['ys.jpg'])  # ['a woman in a hospital bed with a woman in a hospital bed']

print(result)