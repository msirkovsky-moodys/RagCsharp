import { useFormState } from "react-dom";
import styles from "./page.module.css";
import { startPRReview } from "./startPRReview";


export function RepoInput(data: any) {

  const [state, action] = useFormState(startPRReview, null, 'n/a');
  console.log('state', state);

  return <div className={styles.repoInput}>
    <div className={styles.repoInput}>
    
      <form action={action} >
        <div className={styles.repoInput}>
          <input name="pr-number-to-review" placeholder="PR number to review" defaultValue={data?.prNumber}></input>
          <input name="git-repo-name" placeholder="PR repo" defaultValue={data?.repoName}></input>
          <input name="git-personal-token" placeholder="GitHub personal token" defaultValue={data?.personalToken}></input>
          <button>Improve the PR1</button>
        </div>
      </form>
    </div>
    {(state && state.status) && (
        <div className={styles.stateContainer}>
          <p>Suggestion:</p>
          {state.suggestions.map((suggestion: any, i:number) => (
            <div key={i}>
              <p>{suggestion.originalCode}</p>
              <p>{suggestion.newCode}</p>
              <p>--------------------</p>
            </div>
          ))}
        </div>
      )}

  </div>
}