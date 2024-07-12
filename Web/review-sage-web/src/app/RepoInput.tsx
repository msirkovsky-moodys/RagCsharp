import { useFormState } from "react-dom";
import styles from "./page.module.css";
import { startPRReview } from "./startPRReview";
import { useEffect, useState } from "react";



export function RepoInput(data: any) {

  const [state, action] = useFormState(startPRReview, null, 'n/a');
  const [improvingFlag, setImprovingFlag] = useState(false);
  console.log('state', state);
  const handleSubmit = (event:any) => {
    setImprovingFlag(true);
  };

  useEffect(() => {
    if (state != null && improvingFlag == true) {
      setImprovingFlag(false);
    }
  }, [state]); 


  return <div className={styles.prSection}>
    <div >
      <form action={action} onSubmit={handleSubmit}>
        <div className={styles.repoInput}>
          <input name="pr-number-to-review" placeholder="PR number to review" defaultValue={data?.prNumber}></input>
          <input name="git-repo-name" placeholder="PR repo" defaultValue={data?.repoName}></input>
          <input name="git-personal-token" placeholder="GitHub personal token" defaultValue={data?.personalToken}></input>
          <button
            disabled={improvingFlag}
          >
            {improvingFlag ? 'Improving...' : 'Improve the PR'}
            </button>
        </div>
      </form>
    </div>
    <div>
      {(state && state.status && improvingFlag == false) && (
        <div className={styles.stateContainer}>          
          {state.suggestions.map((suggestion: any, i: number) => (
            <div key={i} className={styles.suggestionPanel}>
              <div className={styles.suggestionFileName}>{suggestion.fileName}</div>
              <div className={styles.sidePanel}>
                <div>
                  <span>Existing code:</span>
                  <div>
                    <textarea className={styles.codeTextarea} readOnly={true} value={suggestion.originalCode}></textarea>
                  </div>
                  </div>
                  <div>
                    <span>Suggestion:</span>
                    <div>
                    <textarea  className={styles.codeTextarea} readOnly={true} value={suggestion.newCode}></textarea>                      
                    </div>
                  </div>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  </div>
}