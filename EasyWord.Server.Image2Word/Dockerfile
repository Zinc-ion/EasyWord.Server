FROM myfastapi:v2
WORKDIR /root/EasyWord.Server.Image2Word
COPY EasyWord.Server.Image2Word/main.py .
ENTRYPOINT ["uvicorn", "main:app", "--host", "0.0.0.0"]