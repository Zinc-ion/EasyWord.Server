import io
from fastapi import FastAPI, UploadFile
from transformers import VisionEncoderDecoderModel, ViTImageProcessor, AutoTokenizer
import torch
from PIL import Image

#LocalPath !!!do not use Chinese notes!!!
# model = VisionEncoderDecoderModel.from_pretrained("D://VSCodeField//1models//models--nlpconnect--vit-gpt2-image-captioning//snapshots//dc68f91c06a1ba6f15268e5b9c13ae7a7c514084")
# feature_extractor = ViTImageProcessor.from_pretrained("D://VSCodeField//1models//models--nlpconnect--vit-gpt2-image-captioning//snapshots//dc68f91c06a1ba6f15268e5b9c13ae7a7c514084")
# tokenizer = AutoTokenizer.from_pretrained("D://VSCodeField//1models//models--nlpconnect--vit-gpt2-image-captioning//snapshots//dc68f91c06a1ba6f15268e5b9c13ae7a7c514084")

#DockerPath
model = VisionEncoderDecoderModel.from_pretrained(
     "/root/models/snapshots/dc68f91c06a1ba6f15268e5b9c13ae7a7c514084")
feature_extractor = ViTImageProcessor.from_pretrained(
     "/root/models/snapshots/dc68f91c06a1ba6f15268e5b9c13ae7a7c514084")
tokenizer = AutoTokenizer.from_pretrained(
     "/root/models/snapshots/dc68f91c06a1ba6f15268e5b9c13ae7a7c514084")

device = torch.device("cuda" if torch.cuda.is_available() else "cpu")
model.to(device)

max_length = 16
num_beams = 4
gen_kwargs = {"max_length": max_length, "num_beams": num_beams}


async def predict_step(file: UploadFile):
    request_object_content = await file.read()
    img = Image.open(io.BytesIO(request_object_content))

    pixel_values = feature_extractor(images=[img], return_tensors="pt").pixel_values
    pixel_values = pixel_values.to(device)

    output_ids = model.generate(pixel_values, **gen_kwargs)

    preds = tokenizer.batch_decode(output_ids, skip_special_tokens=True)
    preds = [pred.strip() for pred in preds]
    return preds


app = FastAPI()


@app.post("/")
async def image2text(file: UploadFile):
    return await predict_step(file)

