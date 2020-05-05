using MongoDB.Bson;
using MongoDB.Driver;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataBase : MonoBehaviour
{
    public GameObject inputFieldUserRegister;
    public GameObject inputFieldpasswordRegister;
    public TextMeshProUGUI textDisplayRegister;
    public GameObject inputFieldUserLogin;
    public GameObject inputFieldUserLoginParent;
    public GameObject inputFieldpasswordLogin;
    public Transform contentPanel;
    public TextMeshProUGUI textDisplayLogin;
    public TextMeshProUGUI textDisplayUsuarioLoginOK;
    public TextMeshProUGUI textDisplayCrearCuenta;
    public GameStates stateManager = null;
    public Button prefabBoton;
    public GameObject BtnLogout;
    public GameObject BtnLogin;
    public GameObject BtnRegister;
    public TextMesh recordMesh;
    public String usuarioSave = "unknown";
    public String passwordSave = "";
    public int recordSave = 0;
    public Sprite Image1Login;
    public Sprite Image2Login;
    public Button v_boton_loginUI;
    MongoClient client = new MongoClient("mongodb+srv://daniel:***password***@cluster0-561u6.mongodb.net/test?retryWrites=true&w=majority");
    
    IMongoDatabase database;
    IMongoCollection<BsonDocument> v_RankingCollection;
    IMongoCollection<BsonDocument> v_usuariosCollection;
    // Start is called before the first frame update
    void Start()
    {
        //textDisplayRegister = FindObjectOfType<TextMeshPro>();

        PlayerData data = SaveSystem.LoadPLayer();
        if(data != null)
        {
            Debug.Log(data.username+" rank: "+ data.ranking);
            usuarioSave = data.username;
            recordSave = data.ranking;

            if (usuarioSave == "unknown")
            {
                BtnLogout.active = false;
                textDisplayUsuarioLoginOK.enabled = false;
                v_boton_loginUI.image.overrideSprite = Image1Login;
            }
            else
            {
                inputFieldpasswordLogin.active = false;
                inputFieldUserLoginParent.active = false;
                BtnLogin.active = false;
                textDisplayCrearCuenta.enabled = false;
                BtnRegister.active = false;
                textDisplayLogin.enabled=false;
                v_boton_loginUI.image.overrideSprite = Image2Login;
                textDisplayUsuarioLoginOK.text = "Bienvenido " + usuarioSave;
            }
        }
        else
        {
            BtnLogout.active = false;
            textDisplayUsuarioLoginOK.enabled = false;
        }


        StartCoroutine(RunLaterCoroutine(2));


        database = client.GetDatabase("RankingDB");
        v_RankingCollection = database.GetCollection<BsonDocument>("RankingCollection");
        v_usuariosCollection = database.GetCollection<BsonDocument>("usuarios");
        //SaveScoreToDataBse("paco", 30);
        //GetScoresFromDataBase();
        //UpdateUserScore("s", 2);
    }
    public IEnumerator RunLaterCoroutine( float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        SaveDataToFileAndMongo(recordSave);
    }

    public async void UpdateUserScore(string userName, int sconre)
    {
        //var document = new BsonDocument { { userName, sconre } };
        //await collection.FindOneAndUpdateAsync({ "project": "1" }, { "$set": { "subLot": "2" } });
        var filter = Builders<BsonDocument>.Filter.Eq("username", "Daniel3");
        Debug.Log(v_RankingCollection.Find(filter).FirstOrDefault().ToString());
    }

    public async void SaveScoreToDataBse(string userName, int sconre)
    {
        var document = new BsonDocument { { userName, sconre } };
        await v_RankingCollection.InsertOneAsync(document);
    }

    public async Task<List<BsonDocument>> GetScoresFromDataBase()
    {
        var sort = Builders<BsonDocument>.Sort.Descending("ranking");
        var options = new FindOptions<BsonDocument>
        {
            Sort = sort
        };
        var scoresAwaited = await v_RankingCollection.FindAsync(new BsonDocument(), options);
        //var allScoresTask = v_RankingCollection.FindAsync(new BsonDocument()).Sort(sort);
        //var highestScores = v_RankingCollection.Find(new BsonDocument()).Sort(sort);


        /*var filter = new BsonDocument("username", "paco2");
        var replacementDocument = new BsonDocument { { "username", "Daniel" }, { "ranking", 2 } };
        await v_RankingCollection.FindOneAndReplaceAsync(filter, replacementDocument);
        var allScoresTask = v_RankingCollection.FindAsync(new BsonDocument());
        var scoresAwaited = await allScoresTask;*/

         List <BsonDocument> ranking = new  List<BsonDocument>();
        foreach (var score in scoresAwaited.ToList())
        {
            //anking.Add(Deserialize(score.ToString()));
            //Debug.Log(score["username"]);
            ranking.Add(score);

        }
        return ranking;
    }

    private Ranking Deserialize(string rawJson)
    {
        Debug.Log(rawJson);
        JSONNode pojInfo = JSON.Parse(rawJson);
        Debug.Log(pojInfo["username"].ToString());
        var ranking = new Ranking();
        var stringWithoutID = rawJson.Substring(rawJson.IndexOf("),")+4);
        var username = stringWithoutID.Substring(0, stringWithoutID.IndexOf(":") - 2);
        var score = stringWithoutID.Substring(stringWithoutID.IndexOf(":") + 2, stringWithoutID.IndexOf("}") - stringWithoutID.IndexOf(":") - 3);
        ranking.UserName = username;
        ranking.Score = Convert.ToInt32(score);
        return ranking;
    }

    public async void registrarUsuario()
    {
        var filter = Builders<BsonDocument>.Filter.Eq("username", inputFieldUserRegister.GetComponent<Text>().text);
        var studentDocument = await v_usuariosCollection.Find(filter).FirstOrDefaultAsync();
        if (studentDocument == null)
        {
            if(inputFieldUserRegister.GetComponent<Text>().text != "" && inputFieldpasswordRegister.GetComponent<InputField>().text != "")
            {
                var document = new BsonDocument { { "username", inputFieldUserRegister.GetComponent<Text>().text }, { "password", inputFieldpasswordRegister.GetComponent<InputField>().text}, { "online", false } };
                await v_usuariosCollection.InsertOneAsync(document);
                Debug.Log("insertado nuevo usuario");
                textDisplayRegister.text = "Registro completado";
            }
            else
            {
                textDisplayRegister.text = "¡No dejes campos vacíos!";
            }
                
        }
        else
        {
            textDisplayRegister.text = "¡El usuario ya existe!";
        }
       
    }
    
    public async void LoginUsuario()
    {
        if (inputFieldUserLogin.GetComponent<Text>().text != "" && inputFieldpasswordLogin.GetComponent<InputField>().text != "")
        {
            var filter = Builders<BsonDocument>.Filter.Eq("username", inputFieldUserLogin.GetComponent<Text>().text);
            var studentDocument = await v_usuariosCollection.Find(filter).FirstOrDefaultAsync();
            if (studentDocument != null)
            {
                if(studentDocument["password"]== inputFieldpasswordLogin.GetComponent<InputField>().text)
                {
                    usuarioSave = inputFieldUserLogin.GetComponent<Text>().text;
                    var update = Builders<BsonDocument>.Update.Set("online", true);
                    v_usuariosCollection.UpdateOneAsync(filter, update);
                    Debug.Log("Login correcto");
                    textDisplayLogin.text = "";
                    textDisplayUsuarioLoginOK.text = "Bienvenido " + usuarioSave;
                    inputFieldUserLogin.GetComponent<Text>().text = "";
                    inputFieldpasswordLogin.GetComponent<InputField>().text = "";
                    SaveDataToFileAndMongo(0);
                    inputFieldpasswordLogin.active = false;
                    inputFieldUserLoginParent.active = false;
                    BtnLogout.active = true;
                    textDisplayUsuarioLoginOK.enabled = true;
                    BtnLogin.active = false;
                    textDisplayCrearCuenta.enabled = false;
                    BtnRegister.active = false;
                    textDisplayLogin.enabled = false;
                    v_boton_loginUI.image.overrideSprite = Image2Login;



                    //inputFieldpasswordLogin.SetActive(false);
                    //passwordSave = inputFieldpasswordLogin.GetComponent<InputField>().text;
                }
                else
                {
                    textDisplayLogin.text = "La contraseña es incorrecta. Inténtalo de nuevo.";
                }
            }
            else
            {
                textDisplayLogin.text = "Este nombre de usuario no existe.";
            }
        }
        else
        {
            textDisplayLogin.text = "¡No dejes campos vacíos!";
        }       

    }

     public async void LogoutUsuario()
    {
        usuarioSave = "unknown";
        v_boton_loginUI.image.overrideSprite = Image1Login;
        SaveDataToFileAndMongo(0);
        inputFieldpasswordLogin.active = true;
        inputFieldUserLoginParent.active = true;
        BtnLogout.active = false;
        textDisplayUsuarioLoginOK.enabled = false;
        BtnLogin.active = true;
        textDisplayCrearCuenta.enabled = true;
        BtnRegister.active = true;
        textDisplayLogin.enabled = true;
    }
    


    public async void SaveDataToFileAndMongo(int lvl)
    {
        recordSave = lvl;
        SaveSystem.SavePlayer(new Player(recordSave, usuarioSave));
        if (recordSave == 0)
        {
            recordMesh.text = "";
        }
        else
        {
            recordMesh.text = "Récord: lvl " + recordSave;
        }
        //mongo   
        Debug.Log("subiendo a mongo...");
        var filter = Builders<BsonDocument>.Filter.Eq("username", usuarioSave);
        var studentDocument = await v_RankingCollection.Find(filter).FirstOrDefaultAsync();     
        if (studentDocument == null)
        {
            var document = new BsonDocument { { "username", usuarioSave }, { "ranking", recordSave } };
            v_RankingCollection.InsertOneAsync(document);
            Debug.Log("Ranking subido a mongo");
        }
        else
        {
            //Debug.Log("RECORD!!!!!!!!!!"+ recordSave);
            if (studentDocument["ranking"] < recordSave)
            {
                var update = Builders<BsonDocument>.Update.Set("ranking", recordSave);
                v_RankingCollection.UpdateOneAsync(filter, update);
                Debug.Log("Update ranking subido a mongo");
            }else
            {
                if (usuarioSave != "unknown" && studentDocument["ranking"] > recordSave)
                {
                    SaveDataToFileAndMongo(studentDocument["ranking"].AsInt32);
                    Debug.Log("Ranking no subido, menor o igual que en mongo USUARIO: "+usuarioSave+" RECOGIDO DE MONGO");
                    recordMesh.text = "Récord: lvl " + studentDocument["ranking"].AsInt32;
                }
                else
                {
                    Debug.Log("Ranking no subido, menor o igual que en mongo USUARIO: " + usuarioSave );
                }
            }
        }
    }
    

    public async void PonerListaRanking()
    {

        inputFieldUserLogin.GetComponent<Text>().text = "";
        //delete
        //v_RankingCollection.DeleteManyAsync(new BsonDocument());
        //insert multiple
        /*for (int i = 10; i > 0; i--)
        {
            var document = new BsonDocument { { "username", "paco" }, { "ranking", 3 } };
            await v_RankingCollection.InsertOneAsync(document);
        }*/


        Debug.Log("listaRanking "+ contentPanel.childCount);
        List<BsonDocument> ranking = await GetScoresFromDataBase();
        //Task task = new Task(EliminarBotonesRanking);
        //task.Start();
        //await task;
        //await EliminarBotonesRanking();
        await EliminarBotonesRanking();

        //await Task.Delay(TimeSpan.FromSeconds(0.1));
        var contadorPuesto = 1;
        foreach (var score in ranking)
        {

            Button newButton = Instantiate(prefabBoton) as Button;
            newButton.transform.SetParent(contentPanel);
            newButton.transform.localScale = new Vector3(1, 1, 1);
            newButton.transform.FindChild("TextUsuario").GetComponentInChildren<TMP_Text>().text= score["username"]+"";
            newButton.transform.FindChild("TextRanking").GetComponentInChildren<TMP_Text>().text = score["ranking"] + "";
            newButton.transform.FindChild("TextRankingPos").GetComponentInChildren<TMP_Text>().text = "#"+contadorPuesto;

            if(contadorPuesto > 3)
            {
            botonLista btnlista = newButton.GetComponent<botonLista>();
            btnlista.Setup();
            }

                        contadorPuesto++;
            /*GameObject[] img = GameObject.FindGameObjectsWithTag("imageBotonRanking");
            var v = 0;
            foreach (GameObject imgs in img)
                {
                    if(v > 2)
                    {
                        GameObject.Destroy(imgs);
                    }
                    v++;
                }
            */
            //Debug.Log(score["username"]);

        }
        
    }

    public async Task EliminarBotonesRanking()
    {
        GameObject[] botonesRanking = GameObject.FindGameObjectsWithTag("BotonRanking");
        foreach (GameObject bontonranking in botonesRanking)
             GameObject.Destroy(bontonranking);
       

    }

    public async void PlayOnline()
    {
        if (usuarioSave == "")
        {
            stateManager.loginGame();
        }
    }
}

public class Ranking
{
    public string UserName { get; set; }

    public int Score { get; set; }
}



